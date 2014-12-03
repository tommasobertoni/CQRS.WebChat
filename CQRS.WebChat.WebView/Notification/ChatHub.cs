using CQRS.WebChat.Domain.Entities.Commands;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CQRS.WebChat.WebView.Notification
{
    public class ChatHub : Hub
    {
        public void Talk(Talk talk)
        {
            Clients.All.talk(talk.User, talk.Text, talk.Time);
        }
    }
}