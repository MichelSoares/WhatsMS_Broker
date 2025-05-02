
namespace WhatsMS_Broker.API.DTOs.Request
{
    public class MessageInboundDTO
    {
        //public int Id { get; set; }
        public string IdMessageWhatsApp { get; set; }
        public int AccountId { get; set; }
        public string FromNumber { get; set; }
        public string ToNumber { get; set; }
        public string MessageType { get; set; }
        public string Content { get; set; }
        public DateTime DateReceived { get; set; }
        public bool IsGroup { get; set; }
    }
}
