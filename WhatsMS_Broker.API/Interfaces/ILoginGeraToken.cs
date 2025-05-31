using WhatsMS_Broker.API.DTOs.Request;
using WhatsMS_Broker.API.DTOs.Response;

namespace WhatsMS_Broker.API.Interfaces
{
    public interface ILoginGeraToken
    {
        bool AuthUsuario(LoginGeraTokenDTO loginGeraTokenDTO);
        TokenJwtResponse GerarToken(string emailUsuario);
    }
}
