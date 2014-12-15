using CQRS.WebChat.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.WebChat.Domain.Entities.Commands
{
    [Serializable]
    public class Scream : ICommand
    {
        public string User { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }
    }
}
