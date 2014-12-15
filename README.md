CQRS.WebChat
=========================

Web broadcast chat implemented with CQRS architecture, with COMMANDS, QUERIES and EVENTS. Uses an mvc controller for inserting commands in a queue, then a worker process those commands and notify the webapi who notify the clients that there's a new talkevent, using signalr; then the client request the webapi the message using the username and messageid provided from the previous notification from the webapi.

REQUIRES:
- IIS
- Setting a real azure storage account in the Web.config in the webview project, or azure storage emulator on running machine
