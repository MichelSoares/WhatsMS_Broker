using Microsoft.AspNetCore.Mvc;
using WhatsMS_Broker.API.DTOs.Request;
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

        [HttpGet]
        [Route("check-phonenumber-exists")]
        public async Task<IActionResult> CheckPhoneNumberExists([FromQuery] string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentNullException("informe o número do telefone da instancia client Node!");
            }

            var count = await _clientWhatsMSService.CheckPhoneNumberExists(phoneNumber);
            return Ok(count);
        }

        [HttpGet]
        [Route("check-uptime-qrcode")]
        public async Task<IActionResult> CheckUptimeGenerateQRCode([FromQuery] string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return BadRequest("Informe o número de telefone");

            var genQrcode = await _clientWhatsMSService.CheckUptimeGenerateQRCode(phoneNumber);

            return Ok(new { genQrcode });

        }

        [HttpPut]
        [Route("{phoneNumber}/qrcode")]
        public async Task<IActionResult> SetNewQRCode(string phoneNumber, [FromBody] UpdateQRCodeDTO updateQRCodeDTO)
        {
            if (updateQRCodeDTO == null)
                return BadRequest("Informe os dados do client Node!");

            await _clientWhatsMSService.NewInstanceClientNode(phoneNumber, updateQRCodeDTO);
            return Ok("Dados atualizados com sucesso.");
        }

        [HttpPut]
        [Route("{phoneNumber}/reset-qrcode")]
        public async Task<IActionResult> ResetQRCode(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return BadRequest("Informe o número de telefone");

            await _clientWhatsMSService.ResetQRCode(phoneNumber);
            return Ok("Reset ok.");
        }

        [HttpPut]
        [Route("{phoneNumber}/{sessionId}/new-session")]
        public async Task<IActionResult> NewSessionId(string phoneNumber, string sessionId)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return BadRequest("Informe o número de telefone");

            await _clientWhatsMSService.NewSessionId(phoneNumber, sessionId);
            return Ok("nova sessionId ok");

        }

        [HttpPut]
        [Route("{phoneNumber}/set-uptime-generate-qrcode")]
        public async Task<IActionResult> SetUptimeGenQRCode(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return BadRequest("Informe o número de telefone");

            await _clientWhatsMSService.SetUptimeGenerateQrcode(phoneNumber);
            return Ok("set uptime ok");

        }

        [HttpPut]
        [Route("{phoneNumber}/set-auth")]
        public async Task<IActionResult> SetAuthenticatedPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return BadRequest("Informe o número de telefone");

            await _clientWhatsMSService.SetAuthenticatedPhoneNumber(phoneNumber);
            return Ok("set auth");

        }


    }
}
