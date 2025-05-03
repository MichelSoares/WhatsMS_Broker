using WhatsMS_Broker.API.DTOs.Request;

namespace WhatsMS_Broker.API.Interfaces
{
    public interface IMessageInbound
    {
        Task MessageReceivedAsync(MessageInboundDTO msgInboundDTO);
    }
}
