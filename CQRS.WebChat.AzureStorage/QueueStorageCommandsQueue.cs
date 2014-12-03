using CQRS.WebChat.Domain.Contracts;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.WebChat.AzureStorage
{
    public class QueueStorageCommandsQueue : ICommandsQueue
    {
        private CloudQueue _cloudQueue;

        public QueueStorageCommandsQueue(string storageConnection)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnection);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            _cloudQueue = queueClient.GetQueueReference("messagecommands");
            _cloudQueue.CreateIfNotExists();
        }

        void ICommandsQueue.Push(ICommand command)
        {
            CloudQueueMessage message = new CloudQueueMessage(ICommand.ToByteArray(command));
            _cloudQueue.AddMessage(message);
        }

        ICommand ICommandsQueue.Pop()
        {
            ICommand command = null;
            CloudQueueMessage message = _cloudQueue.GetMessage();
            if (message != null)
            {
                _cloudQueue.DeleteMessage(message);
                command = ICommand.FromByteArray(message.AsBytes);
            }

            return command;
        }
    }
}
