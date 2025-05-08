using WhatsMS_Broker.API.DTOs.Request;
using WhatsMS_Broker.API.Interfaces;
using WhatsMS_Broker.API.Utils;
using WhatsMS_Broker.Data.Context;
using WhatsMS_Broker.Domain.Entidades;

namespace WhatsMS_Broker.API.Services
{
    public class MessageOutboundService : IMessageOutbound
    {
        private readonly BrokerDbContext _brokerDbContext;
        private readonly IMessageSendAppNode _sender;

        public MessageOutboundService(BrokerDbContext brokerDbContext, IMessageSendAppNode sender)
        {
            _brokerDbContext = brokerDbContext;
            _sender = sender;
        }
        public async Task SendMessageAsync(MessageOutboundDTO msgOutboundDTO)
        {
            var msgOut = await _sender.SendToNodeAsync(msgOutboundDTO);

            if (!string.IsNullOrEmpty(msgOut.id))
            {
                var message = new MessageOutbound
                {
                    IdMsg = msgOut.id,
                    AccountId = msgOutboundDTO.AccountId,
                    CreatedAt = DateTime.UtcNow,
                    SentAt = Helper.FormatUnixTimeToDate(msgOut.timestamp.ToString()),
                    FromNumber = msgOutboundDTO.FromNumber,
                    ToNumber = msgOutboundDTO.ToNumber,
                    MessageType = msgOutboundDTO.MessageType,
                    Content = msgOutboundDTO.Content,
                    Type = msgOutboundDTO.Type,
                    MidiaContentType = msgOutboundDTO.MidiaContentType,
                    MidiaURL = msgOutboundDTO.MidiaURL,
                    Latitude = msgOutboundDTO.Latitude,
                    Longitude = msgOutboundDTO.Longitude,
                    IsGroup = false,

                };

                await _brokerDbContext.MessageOutbounds.AddAsync(message);
                await _brokerDbContext.SaveChangesAsync();
            }
            
        }
    }
}
