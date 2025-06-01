using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsMS_Broker.Application.Common.Results.WhatsMS_Broker.Application.Common.Results;
using WhatsMS_Broker.Application.DTOs.Commands;
using WhatsMS_Broker.Application.DTOs.Responses;

namespace WhatsMS_Broker.Application.Interfaces
{
    public interface ILoginGeraToken
    {
        //bool AuthUsuario(LoginGeraTokenDTO loginGeraTokenDTO);
        //TokenJwtResponse GerarToken(string emailUsuario);

        Task<Result<AuthTokenResponse>> AuthenticateAsync(LoginCommand command);
    }
}
