using Microsoft.AspNetCore.Mvc;
using WhatsMS_Broker.API.DTOs.Request;
using WhatsMS_Broker.API.Interfaces;

namespace WhatsMS_Broker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageOutboundController : Controller
    {
        private readonly ILogger<MessageOutboundController> _logger;
        private readonly IMessageOutbound _messageOutbound;
        public MessageOutboundController(ILogger<MessageOutboundController> logger, IMessageOutbound messageOutbound)
        {
            _logger = logger;
            _messageOutbound = messageOutbound;
        }

        [HttpPost]
        [Route("SendMessage/")]
        public async Task<IActionResult> SendMessage([FromBody] MessageOutboundDTO msgOutobund)
        {
            _logger.LogInformation("Mensagem de saida!");

            if (msgOutobund == null)
            {
                return BadRequest("Mensagem inválida.");
            }

            await _messageOutbound.SendMessageAsync(msgOutobund);

            return Ok("Mensagem enviada com sucesso.");
        }
    }
}
