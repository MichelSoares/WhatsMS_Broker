using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsMS_Broker.Domain.Entidades
{
    public class MessageStatus
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public ICollection<MessageOutbound> MessageOutbounds { get; set; }
    }
}
