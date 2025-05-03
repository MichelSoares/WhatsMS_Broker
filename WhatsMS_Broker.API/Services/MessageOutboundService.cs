using WhatsMS_Broker.API.DTOs.Request;
using WhatsMS_Broker.API.Interfaces;
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
            var message = new MessageOutbound 
            {
                IdMsg = msgOutboundDTO.IdMsg,
                AccountId = msgOutboundDTO.AccountId,
                CreatedAt = msgOutboundDTO.CreatedAt,
                SentAt = msgOutboundDTO.SentAt,
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

            await _sender.SendToNodeAsync(msgOutboundDTO);
        }
    }
}
