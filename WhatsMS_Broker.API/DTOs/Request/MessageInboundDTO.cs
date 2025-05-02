
namespace WhatsMS_Broker.API.DTOs.Request
{
    public class MessageInboundDTO
    {
        //public int Id { get; set; }
        public string IdMessageWhatsApp { get; set; }
        public int AccountId { get; set; }
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
    }
}
