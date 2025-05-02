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
        public string IdMessageWhatsApp { get; set; }
        public int AccountId { get; set; }
        public AccountMS Account { get; set; }

        public DateTime DateReceived { get; set; }
        public string FromNumber { get; set; }
        public string ToNumber { get; set; }
        public string MessageType { get; set; }
        public string Content { get; set; }
        public string? Type { get; set; }
        public string? MidiaContentType { get; set; }
        public string? MidiaURL { get; set; }
        public string? ProfileName { get; set; }
        public string? NotifyName { get; set; }
        public string? Author { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsForwarded { get; set; }
        public int? ForwardingScore { get; set; }
        public bool IsGroup { get; set; }
        public bool IsColetado { get; set; }
    }
}
