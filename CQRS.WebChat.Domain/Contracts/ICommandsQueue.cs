using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.WebChat.Domain.Contracts
{
    public interface ICommandsQueue
    {
        void Push(ICommand command);

        ICommand Pop();
    }
}
