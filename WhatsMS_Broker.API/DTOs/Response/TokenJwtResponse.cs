using Newtonsoft.Json.Linq;

namespace WhatsMS_Broker.API.DTOs.Response
{
    public class TokenJwtResponse
    {
        public string token { get; set; }
        public string expiresAt { get; set; }

        public TokenJwtResponse(string token, string expiresAt)
        {
            this.token = token;
            this.expiresAt = expiresAt;
        }
    }
}
