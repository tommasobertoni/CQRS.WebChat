using CQRS.WebChat.AzureStorage;
using CQRS.WebChat.Domain.Contracts;
using CQRS.WebChat.Domain.Entities.Events;
using CQRS.WebChat.Domain.Entities.Queries;
using CQRS.WebChat.QueriesHandling;
using CQRS.WebChat.WebView.Notification;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace CQRS.WebChat.WebView.Controllers
{
    //[System.Web.Http.Authorize]
    public class ChatApiController : ApiController
    {
        private IQueriesHandler _queriesHandler;

        public ChatApiController() : base()
        {
            string storageConnection = ConfigurationManager.ConnectionStrings["storageConnection"].ConnectionString;
            _queriesHandler = new AzureQueriesHandler(storageConnection);
        }

        public MessageSimpleTime Get(string user, string id, int type)
        {
            MessageSimpleTime message = null;
            switch (type)
            {
                case 0:
                    message = _queriesHandler.GetTalk(user, id);
                    break;

                case 1:
                    message = _queriesHandler.GetScream(user, id);
                    break;
            }

            return message;
        }

        public void Post(MessageEvent messageEvent)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();

            switch (messageEvent.Type)
            {
                case 0:
                    var talkEvent = new TalkEvent
                    {
                        Id = messageEvent.Id,
                        User = messageEvent.User
                    };
                    hub.Clients.All.talk(talkEvent);
                    break;

                case 1:
                    var screamEvent = new ScreamEvent
                    {
                        Id = messageEvent.Id,
                        User = messageEvent.User
                    };
                    hub.Clients.All.scream(screamEvent);
                    break;
            }
        }
    }
}
