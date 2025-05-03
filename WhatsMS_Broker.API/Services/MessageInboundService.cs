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
        public async Task MessageReceivedAsync(MessageInboundDTO msgDTO)
        {
            var message = new MessageInbound
            {
                AccountId = msgDTO.AccountId,
                DateReceived = msgDTO.DateReceived,
                IdMessageWhatsApp = msgDTO.IdMessageWhatsApp,
                FromNumber = msgDTO.FromNumber,
                ToNumber = msgDTO.ToNumber,
                MessageType = msgDTO.MessageType,
                Content = msgDTO.Content,
                Type = msgDTO.Type,
                MidiaContentType = msgDTO.MidiaContentType,
                MidiaURL = msgDTO.MidiaURL,
                ProfileName = msgDTO.ProfileName,
                NotifyName = msgDTO.NotifyName,
                Author = msgDTO.Author,
                Latitude = msgDTO.Latitude,
                Longitude = msgDTO.Longitude,
                IsForwarded = msgDTO.IsForwarded,
                IsColetado = false,
                IsGroup = msgDTO.IsGroup
            };

            await _brokerDbContext.MessageInbounds.AddAsync(message);
            
            await _brokerDbContext.SaveChangesAsync();
        }
    }
}
