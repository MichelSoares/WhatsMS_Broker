using WhatsMS_Broker.API.DTOs.Request;
using WhatsMS_Broker.API.Interfaces;

namespace WhatsMS_Broker.API.Services
{
    public class MessageSendAppNodeService : IMessageSendAppNode
    {
        private readonly HttpClient _httpClient;

        public MessageSendAppNodeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task SendToNodeAsync(MessageOutboundDTO msg)
        {
            var response = await _httpClient.PostAsJsonAsync("/send-message", msg);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao chamar Node API: {response.StatusCode} - {error}");
            }
        }
    }
}
