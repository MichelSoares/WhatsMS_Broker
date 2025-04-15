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
    public class MessageOutboundRepository : IMessageOutboundRepository
    {
        private readonly BrokerDbContext _context;

        public MessageOutboundRepository(BrokerDbContext context)
        {
            _context = context;
        }
        public Task AddOutboundMessageAsync(MessageInbound message)
        {
            throw new NotImplementedException();
        }
    }
}
