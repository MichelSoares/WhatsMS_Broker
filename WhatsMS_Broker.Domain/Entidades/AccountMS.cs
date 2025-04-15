using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsMS_Broker.Domain.Entidades
{
    public class AccountMS
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string PhoneNumber { get; set; }
        public string SessionName { get; set; }
        public string WebhookUrl { get; set; }
        public string AuthToken { get; set; }
        public string PortRun { get; set; }
        public string QrCodeBase64 { get; set; }
        public bool IsActive { get; set; }
        public string ClientSessionID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
