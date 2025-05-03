using WhatsMS_Broker.API.DTOs.Request;

namespace WhatsMS_Broker.API.Interfaces
{
    public interface IMessageOutbound
    {
        Task SendMessageAsync(MessageOutboundDTO msgOutboundDTO);
    }
}
