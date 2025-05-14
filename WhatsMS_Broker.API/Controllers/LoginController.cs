using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhatsMS_Broker.API.DTOs.Request;
using WhatsMS_Broker.API.Interfaces;

namespace WhatsMS_Broker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginGeraToken _geraToken;

        public LoginController(ILogger<LoginController> logger, ILoginGeraToken geraToken)
        {
            _logger = logger;
            _geraToken = geraToken;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult LoginGeraToken([FromBody] LoginGeraTokenDTO loginGeraTokenDTO)
        {
            if (loginGeraTokenDTO == null || string.IsNullOrWhiteSpace(loginGeraTokenDTO.Email) || string.IsNullOrWhiteSpace(loginGeraTokenDTO.Senha))
            {
                return BadRequest("Dados de login inválidos.");
            }

            try
            {
                var isAuth = _geraToken.AuthUsuario(loginGeraTokenDTO);

                if (!isAuth)
                {
                    return Unauthorized("Usuário ou senha inválidos.");
                }

                var tokenJwt = _geraToken.GerarToken(loginGeraTokenDTO.Email);

                return Ok(new
                {
                    token = tokenJwt,
                    email = loginGeraTokenDTO.Email
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar token JWT.");
                return StatusCode(500, "Erro interno ao processar autenticação.");
            }
        }
    }
}
