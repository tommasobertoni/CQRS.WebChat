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

namespace CQRS.WebChat.WebView.Controllers
{
    public class ChatApiController : ApiController
    {
        private IQueriesHandler _queriesHandler;

        public ChatApiController() : base()
        {
            string storageConnection = ConfigurationManager.ConnectionStrings["storageConnection"].ConnectionString;
            _queriesHandler = new AzureQueriesHandler(storageConnection);
        }

        [HttpGet]
        [ActionName("Talk")]
        public TalkSimpleTime QueryTalk(string user, string id)
        {
            return _queriesHandler.GetTalk(user, id);
        }

        [HttpPost]
        [ActionName("Talk")]
        public void TalkEvent(TalkEvent talkEvent)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            hub.Clients.All.talk(talkEvent);
        }

        [HttpGet]
        [ActionName("Scream")]
        public ScreamSimpleTime QueryScream(string user, string id)
        {
            return _queriesHandler.GetScream(user, id);
        }

        [HttpPost]
        [ActionName("Scream")]
        public void ScreamEvent(TalkEvent talkEvent)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            hub.Clients.All.scream(talkEvent);
        }
    }
}
