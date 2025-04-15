using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsMS_Broker.Domain.Entidades
{
    public class MessageInbound
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public AccountMS Account { get; set; }

        public string FromNumber { get; set; }
        public string ToNumber { get; set; }
        public string MessageType { get; set; }
        public string Content { get; set; }
        public DateTime DateReceived { get; set; }
        public bool IsGroup { get; set; }
    }
}
