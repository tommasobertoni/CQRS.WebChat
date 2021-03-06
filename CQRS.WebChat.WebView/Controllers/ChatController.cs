﻿using CQRS.WebChat.CommandsHandling;
using CQRS.WebChat.Domain.Contracts;
using CQRS.WebChat.Domain.Entities.Commands;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CQRS.WebChat.WebView.Controllers
{
    public class ChatController : Controller
    {
        private ICommandsHandler _commandsHandler;

        public string TestUser { get; set; }

        public ChatController() : base()
        {
            string storageConnection = ConfigurationManager.ConnectionStrings["storageConnection"].ConnectionString;
            _commandsHandler = new AzureCommandsHandler(storageConnection);
            TestUser = "testuser";
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Talk")]
        public ActionResult Handle(Talk command)
        {
            command.User = TestUser;
            command.Time = DateTime.Now.ToUniversalTime();
            _commandsHandler.Handle(command);
            return new HttpStatusCodeResult(202);
        }

        [HttpPost]
        [ActionName("Scream")]
        public ActionResult Handle(Scream command)
        {
            command.User = TestUser;
            command.Time = DateTime.Now.ToUniversalTime();
            _commandsHandler.Handle(command);
            return new HttpStatusCodeResult(202);
        }
    }
}
