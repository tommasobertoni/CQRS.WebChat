using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.WebChat.Domain.Entities.Queries
{
    public class ScreamSimpleTime
    {
        public string User { get; set; }

        public string Text { get; set; }

        public string SimpleTime { get; set; }
    }
}
