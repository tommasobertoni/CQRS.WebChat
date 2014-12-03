using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.WebChat.Domain.Entities.Queries
{
    public class TalkSimpleTime
    {
        public string User { get; set; }

        public string Text { get; set; }

        public string Time { get; set; }
    }
}
