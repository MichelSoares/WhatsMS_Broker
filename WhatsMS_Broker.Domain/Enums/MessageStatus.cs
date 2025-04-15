using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsMS_Broker.Domain.Enums
{
    public enum MessageStatus
    {
        Pendente = 0,
        Enviada = 1,
        Entregue = 2,
        Lida = 3,
        Erro = 4
    }
}
