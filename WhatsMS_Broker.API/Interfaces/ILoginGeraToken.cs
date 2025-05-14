using WhatsMS_Broker.API.DTOs.Request;

namespace WhatsMS_Broker.API.Interfaces
{
    public interface ILoginGeraToken
    {
        bool AuthUsuario(LoginGeraTokenDTO loginGeraTokenDTO);
        string GerarToken(string emailUsuario);
    }
}
