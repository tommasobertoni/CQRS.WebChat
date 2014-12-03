using CQRS.WebChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.WebChat.Domain.Contracts
{
    public interface IMessageRepository
    {
        Message GetById(string user, string id);

        void Insert(Message message);
    }
}
