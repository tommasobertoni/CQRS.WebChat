using CQRS.WebChat.AzureStorage;
using CQRS.WebChat.Domain.Contracts;
using CQRS.WebChat.Domain.Entities;
using CQRS.WebChat.Domain.Entities.Commands;
using CQRS.WebChat.Domain.Entities.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.WebChat.Worker
{
    public class CommandsPerformer
    {
        private IMessageRepository _repository;
        private ICommandsQueue _queue;
        private HttpClient _httpClient;

        public bool IsWorking { get; private set; }

        private const int DEFAULT_WAITING_TIME = 500;
        private const int MAX_WAITING_TIME = 5000;
        private const int ATTEMPTS = 12;

        public CommandsPerformer(string storageConnection)
        {
            _repository = new TableStorageRepository(storageConnection);
            _queue = new QueueStorageCommandsQueue(storageConnection);
            _httpClient = new HttpClient();
        }

        public void StartWorking()
        {
            if (!IsWorking)
            {
                Log("~ starting perfomer");
                IsWorking = true;
                new Thread(Run).Start();
            }
        }

        private void Run()
        {
            Log("~ performer starts running");
            int waitingTime = DEFAULT_WAITING_TIME;
            int currentAttempt = -1;
            while (IsWorking)
            {
                var command = _queue.Pop();
                if (command != null)
                {
                    currentAttempt = 0;
                    waitingTime = DEFAULT_WAITING_TIME;

                    try
                    {
                        if (command is Talk)
                        {
                            PerformCommand(command as Talk);
                        }
                        else if (command is Scream)
                        {
                            PerformCommand(command as Scream);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log(String.Format("Exception occurred:\n{0}\n", ex.Message), ConsoleColor.Red);
                    }
                }
                else
                {
                    currentAttempt++;
                }

                if (currentAttempt >= ATTEMPTS)
                {
                    currentAttempt = 0;
                    if (waitingTime < MAX_WAITING_TIME)
                        waitingTime += 250;
                    Log("~ Increase waiting time to " + waitingTime, ConsoleColor.DarkYellow);
                }

                Thread.Sleep(waitingTime);
            }

            Log("~ performer stopped");
        }

        public void StopWorking()
        {
            Log("~ stopping perfomer");
            IsWorking = false;
        }

        private void Log(string message)
        {
            Log(message, ConsoleColor.DarkGray);
        }

        private void Log(string message, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = oldColor;
        }

        private void PerformCommand(Talk command)
        {
            Message message = new Message
            {
                Id = Guid.NewGuid().ToString(),
                User = command.User,
                Text = command.Text,
                Time = command.Time,
                Type = 0
            };

            _repository.Insert(message);
            Log("~ new message from " + message.User + ": " + message.Text, ConsoleColor.Green);

            MessageEvent messageEvent = new MessageEvent
            {
                Id = message.Id,
                User = message.User,
                Type = 0
            };

            var byteArrayContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageEvent)));
            byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var task = _httpClient.PostAsync("http://localhost:32349/api/chatapi", byteArrayContent);
            //task.Wait();
        }

        private void PerformCommand(Scream command)
        {
            Message message = new Message
            {
                Id = Guid.NewGuid().ToString(),
                User = command.User,
                Text = command.Text,
                Time = command.Time,
                Type = 1
            };

            _repository.Insert(message);
            Log("~ new scream message from " + message.User + ": " + message.Text, ConsoleColor.Green);

            MessageEvent messageEvent = new MessageEvent
            {
                Id = message.Id,
                User = message.User,
                Type = 1
            };

            var byteArrayContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageEvent)));
            byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var task = _httpClient.PostAsync("http://localhost:32349/api/chatapi", byteArrayContent);
            //task.Wait();
        }
    }
}
