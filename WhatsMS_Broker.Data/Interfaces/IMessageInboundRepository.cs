using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsMS_Broker.Domain.Entidades;

namespace WhatsMS_Broker.Data.Interfaces
{
    public interface IMessageInboundRepository
    {
        Task AddInboundMessageAsync(MessageInbound message);
    }
}
