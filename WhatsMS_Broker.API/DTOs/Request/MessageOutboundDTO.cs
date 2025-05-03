using WhatsMS_Broker.Domain.Entidades;
using WhatsMS_Broker.Domain.Enums;

namespace WhatsMS_Broker.API.DTOs.Request
{
    public class MessageOutboundDTO
    {
        //public int Id { get; set; }
        public string IdMsg { get; set; }
        public int AccountId { get; set; }
        //public AccountMS Account { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime SentAt { get; set; }
        public string FromNumber { get; set; }
        public string ToNumber { get; set; }
        public string MessageType { get; set; }
        public string Content { get; set; }

        public string? Type { get; set; }
        public string? MidiaContentType { get; set; }
        public string? MidiaURL { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsGroup { get; set; }

        public MessageStatus Status { get; set; }
    }
}
