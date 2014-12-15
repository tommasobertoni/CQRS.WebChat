using CQRS.WebChat.Domain.Contracts;
using CQRS.WebChat.Domain.Entities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.WebChat.AzureStorage
{
    public class TableStorageRepository : IMessageRepository
    {
        private CloudTable _cloudTable;

        public TableStorageRepository(string storageConnection)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnection);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            _cloudTable = tableClient.GetTableReference("webchat");
            _cloudTable.CreateIfNotExists();
        }

        Message IMessageRepository.GetById(string user, string id)
        {
            MessageTableEntity mte = GetMessageTableEntityById(user, id);
            return new Message
            {
                Id = mte.Id,
                User = mte.User,
                Text = mte.Text,
                Time = mte.Time,
                Type = mte.Type
            };
        }

        private MessageTableEntity GetMessageTableEntityById(string user, string id)
        {
            TableOperation getToDoItemTableEntityById = TableOperation.Retrieve<MessageTableEntity>(user, id);
            return _cloudTable.Execute(getToDoItemTableEntityById).Result as MessageTableEntity;
        }

        void IMessageRepository.Insert(Message message)
        {
            MessageTableEntity mte = (MessageTableEntity)message;

            TableOperation insert = TableOperation.Insert(mte);
            _cloudTable.Execute(insert);
        }
    }

    class MessageTableEntity : TableEntity
    {
        public string Id { get; set; }

        public string User { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }

        public int Type { get; set; }

        public static explicit operator MessageTableEntity(Message message)
        {
            return new MessageTableEntity
            {
                PartitionKey = message.User,
                RowKey = message.Id,
                Id = message.Id,
                User = message.User,
                Text = message.Text,
                Time = message.Time,
                Type = message.Type
            };
        }
    }
}
