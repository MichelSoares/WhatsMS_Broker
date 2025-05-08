using System.Text.Json;
using WhatsMS_Broker.API.DTOs.Request;
using WhatsMS_Broker.API.DTOs.Response;
using WhatsMS_Broker.API.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WhatsMS_Broker.API.Services
{
    public class MessageSendAppNodeService : IMessageSendAppNode
    {
        private readonly HttpClient _httpClient;

        public MessageSendAppNodeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<SendMessageResponse> SendToNodeAsync(MessageOutboundDTO msg)
        {
            var response = await _httpClient.PostAsJsonAsync("/send-message", msg);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao chamar Node API: {response.StatusCode} - {error}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            //var retSend = JsonConvert.DeserializeObject<SendMessageResponse>(responseContent);
            var retSend = JsonSerializer.Deserialize<SendMessageResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return retSend ?? throw new Exception("Resposta inválida da APP Node...");
        }
    }
}
