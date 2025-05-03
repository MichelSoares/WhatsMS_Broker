using WhatsMS_Broker.API.DTOs.Request;

namespace WhatsMS_Broker.API.Interfaces
{
    public interface IMessageSendAppNode
    {
        Task SendToNodeAsync(MessageOutboundDTO msg);
    }
}
