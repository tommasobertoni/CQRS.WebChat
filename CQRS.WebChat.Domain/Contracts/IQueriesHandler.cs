using CQRS.WebChat.Domain.Entities.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.WebChat.Domain.Contracts
{
    public interface IQueriesHandler
    {
        TalkSimpleTime GetTalk(string user, string id);
    }
}
