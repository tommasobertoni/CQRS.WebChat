using CQRS.WebChat.AzureStorage;
using CQRS.WebChat.Domain.Contracts;
using CQRS.WebChat.Domain.Entities.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.WebChat.QueriesHandling
{
    public class AzureQueriesHandler : IQueriesHandler
    {
        private IMessageRepository _repository;

        public AzureQueriesHandler(string storageConnection)
        {
            _repository = new TableStorageRepository(storageConnection);
        }

        TalkSimpleTime IQueriesHandler.GetTalk(string user, string id)
        {
            var message = _repository.GetById(user, id);

            if (message == null || message.Type != 0) return null;

            return new TalkSimpleTime
            {
                User = message.User,
                Text = message.Text,
                Time = message.Time.ToLocalTime().ToString("HH:mm:ss")
            };
        }

        ScreamSimpleTime IQueriesHandler.GetScream(string user, string id)
        {
            var message = _repository.GetById(user, id);

            if (message == null || message.Type != 1) return null;

            return new ScreamSimpleTime
            {
                User = message.User,
                Text = message.Text,
                Time = message.Time.ToLocalTime().ToString("HH:mm:ss")
            };
        }
    }
}
