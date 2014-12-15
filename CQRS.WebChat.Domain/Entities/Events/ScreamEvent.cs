using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.WebChat.Domain.Entities.Events
{
    public class ScreamEvent
    {
        public string Id { get; set; }

        public string User { get; set; }
    }
}
