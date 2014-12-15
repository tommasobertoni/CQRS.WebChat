using CQRS.WebChat.AzureStorage;
using CQRS.WebChat.Domain.Contracts;
using CQRS.WebChat.Domain.Entities.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.WebChat.CommandsHandling
{
    public class AzureCommandsHandler : ICommandsHandler
    {
        private ICommandsQueue _commandsQueue;

        public AzureCommandsHandler(string storageConnection)
        {
            _commandsQueue = new QueueStorageCommandsQueue(storageConnection);
        }

        void ICommandsHandler.Handle(Talk command)
        {
            _commandsQueue.Push(command);
        }

        void ICommandsHandler.Handle(Scream command)
        {
            _commandsQueue.Push(command);
        }
    }
}
