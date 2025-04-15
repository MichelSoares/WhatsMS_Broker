using Microsoft.AspNetCore.Mvc;
using WhatsMS_Broker.API.Interfaces;
using WhatsMS_Broker.API.Services;
using WhatsMS_Broker.Data.Context;

namespace WhatsMS_Broker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientWhatsMSController : Controller
    {
        private readonly IClientWhatsMSService _clientWhatsMSService;
        private readonly ILogger<ClientWhatsMSController> _logger;

        public ClientWhatsMSController(IClientWhatsMSService clientWhatsMSService, ILogger<ClientWhatsMSController> logger)
        {
            _clientWhatsMSService = clientWhatsMSService;
            _logger = logger;
        }

        [HttpGet]
        [Route("check-status/")]
        public async Task<IActionResult> CheckStatusClient([FromQuery] string phoneNumber)
        {
            _logger.LogInformation("Teste");
            if (string.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentNullException("informe o número do telefone da instancia client Node!");
            }

            var result = await _clientWhatsMSService.CheckStatusByPhoneNumberAsync(phoneNumber);
            if (result == null) return NotFound();

            return Ok(result);
        }

    }
}
