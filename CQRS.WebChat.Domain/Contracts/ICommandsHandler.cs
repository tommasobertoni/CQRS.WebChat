using CQRS.WebChat.Domain.Entities.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.WebChat.Domain.Contracts
{
    public interface ICommandsHandler
    {
        void Handle(Talk command);
    }
}
