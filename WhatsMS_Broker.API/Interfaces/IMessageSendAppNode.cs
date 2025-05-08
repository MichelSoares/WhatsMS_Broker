using WhatsMS_Broker.API.DTOs.Request;
using WhatsMS_Broker.API.DTOs.Response;

namespace WhatsMS_Broker.API.Interfaces
{
    public interface IMessageSendAppNode
    {
        Task<SendMessageResponse> SendToNodeAsync(MessageOutboundDTO msg);
    }
}
