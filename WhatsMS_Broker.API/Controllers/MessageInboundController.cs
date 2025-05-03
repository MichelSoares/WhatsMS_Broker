using Microsoft.AspNetCore.Mvc;
using WhatsMS_Broker.API.DTOs.Request;
using WhatsMS_Broker.API.Interfaces;

namespace WhatsMS_Broker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageInboundController : Controller
    {
        private readonly ILogger<MessageInboundController> _logger;
        private readonly IMessageInbound _messageInbound;
        public MessageInboundController(ILogger<MessageInboundController> logger, IMessageInbound messageInbound)
        {
            _logger = logger;
            _messageInbound = messageInbound;
        }

        [HttpPost]
        [Route("MessageReceived/")]
        public async Task<IActionResult> MessageInboundReceived([FromBody] MessageInboundDTO msgInbound)
        {
            _logger.LogInformation("Mensagem de entrada!");

            if (msgInbound == null)
            {
                return BadRequest("Mensagem inválida.");
            }

            await _messageInbound.MessageReceivedAsync(msgInbound);

            return Ok("Mensagem recebida com sucesso.");
        }
    }
}
