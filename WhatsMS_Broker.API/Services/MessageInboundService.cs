using WhatsMS_Broker.API.DTOs.Request;
using WhatsMS_Broker.API.Interfaces;
using WhatsMS_Broker.Data.Context;
using WhatsMS_Broker.Domain.Entidades;

namespace WhatsMS_Broker.API.Services
{
    public class MessageInboundService : IMessageInbound
    {
        private readonly BrokerDbContext _brokerDbContext;

        public MessageInboundService(BrokerDbContext brokerDbContext)
        {
            _brokerDbContext = brokerDbContext;
        }
        public async Task MessageReceived(MessageInboundDTO msgDTO)
        {
            var message = new MessageInbound
            {
                AccountId = msgDTO.AccountId,
                IdMessageWhatsApp = msgDTO.IdMessageWhatsApp,
                FromNumber = msgDTO.FromNumber,
                ToNumber = msgDTO.ToNumber,
                MessageType = msgDTO.MessageType,
                Content = msgDTO.Content,
                DateReceived = msgDTO.DateReceived,
                IsGroup = msgDTO.IsGroup
            };

            await _brokerDbContext.MessageInbounds.AddAsync(message);
            
            await _brokerDbContext.SaveChangesAsync();
        }
    }
}
