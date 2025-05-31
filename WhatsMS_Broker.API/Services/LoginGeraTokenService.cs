using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WhatsMS_Broker.API.DTOs.Request;
using WhatsMS_Broker.API.DTOs.Response;
using WhatsMS_Broker.API.Interfaces;
using WhatsMS_Broker.Data.Context;

namespace WhatsMS_Broker.API.Services
{
    public class LoginGeraTokenService : ILoginGeraToken
    {
        private readonly IConfiguration _configuration;
        private readonly BrokerDbContext _brokerDbContext;

        public LoginGeraTokenService(IConfiguration configuration, BrokerDbContext brokerDbContext)
        {
            _configuration = configuration;
            _brokerDbContext = brokerDbContext;
        }

        public bool AuthUsuario(LoginGeraTokenDTO loginGeraTokenDTO)
        {
            // TODO: aplicar hash na senha...vou colocar em MD5 em um ssegundo momento
            return _brokerDbContext.Clientes
                .Any(x => x.Email == loginGeraTokenDTO.Email && x.Password == loginGeraTokenDTO.Senha);
        }

        public TokenJwtResponse GerarToken(string emailUsuario)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:keySecret"]);
            var tokenHandler = new JwtSecurityTokenHandler();

            var timeZoneBr = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            var dataAtualBrasilia = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneBr);
            var expireToken = dataAtualBrasilia.AddHours(Convert.ToDouble(_configuration["Jwt:expireTokenInHours"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                 new Claim(ClaimTypes.Name, emailUsuario)
                }),

                NotBefore = dataAtualBrasilia,
                IssuedAt = dataAtualBrasilia,
                Expires = expireToken,
                Issuer = _configuration["Jwt:issuer"],
                Audience = _configuration["Jwt:audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var retToken = tokenHandler.WriteToken(token);

            return new TokenJwtResponse(retToken, expireToken.ToString());
        }

    }
}
