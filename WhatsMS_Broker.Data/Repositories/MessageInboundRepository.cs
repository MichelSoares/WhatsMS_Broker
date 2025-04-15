using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsMS_Broker.Data.Context;
using WhatsMS_Broker.Data.Interfaces;
using WhatsMS_Broker.Domain.Entidades;

namespace WhatsMS_Broker.Data.Repositories
{
    public class MessageInboundRepository : IMessageInboundRepository
    {
        private readonly BrokerDbContext _context;

        public MessageInboundRepository(BrokerDbContext context)
        {
            _context = context;
        }

        public async Task AddInboundMessageAsync(MessageInbound message)
        {
            _context.MessageInbounds.Add(message);
            await _context.SaveChangesAsync();
        }
    }
}
