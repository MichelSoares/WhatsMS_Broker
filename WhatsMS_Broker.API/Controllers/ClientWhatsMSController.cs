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

            var count = await _clientWhatsMSService.CheckPhoneNumberExistsAsync(phoneNumber);
            return Ok(count);
        }

        [HttpGet]
        [Route("check-uptime-qrcode")]
        public async Task<IActionResult> CheckUptimeGenerateQRCode([FromQuery] string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return BadRequest("Informe o número de telefone");

            var genQrcode = await _clientWhatsMSService.CheckUptimeGenerateQRCodeAsync(phoneNumber);

            return Ok(new { genQrcode });

        }

        [HttpPut]
        [Route("{phoneNumber}/qrcode")]
        public async Task<IActionResult> SetNewQRCode(string phoneNumber, [FromBody] UpdateQRCodeDTO updateQRCodeDTO)
        {
            if (updateQRCodeDTO == null)
                return BadRequest("Informe os dados do client Node!");

            await _clientWhatsMSService.NewInstanceClientNodeAsync(phoneNumber, updateQRCodeDTO);
            return Ok("Dados atualizados com sucesso.");
        }

        [HttpPut]
        [Route("{phoneNumber}/reset-qrcode")]
        public async Task<IActionResult> ResetQRCode(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return BadRequest("Informe o número de telefone");

            await _clientWhatsMSService.ResetQRCodeAsync(phoneNumber);
            return Ok("Reset ok.");
        }

        [HttpPut]
        [Route("{phoneNumber}/{sessionId}/new-session")]
        public async Task<IActionResult> NewSessionId(string phoneNumber, string sessionId)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return BadRequest("Informe o número de telefone");

            await _clientWhatsMSService.NewSessionIdAsync(phoneNumber, sessionId);
            return Ok("nova sessionId ok");

        }

        [HttpPut]
        [Route("{phoneNumber}/set-uptime-generate-qrcode")]
        public async Task<IActionResult> SetUptimeGenQRCode(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return BadRequest("Informe o número de telefone");

            await _clientWhatsMSService.SetUptimeGenerateQrcodeAsync(phoneNumber);
            return Ok("set uptime ok");

        }

        [HttpPut]
        [Route("{phoneNumber}/set-auth")]
        public async Task<IActionResult> SetAuthenticatedPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return BadRequest("Informe o número de telefone");

            await _clientWhatsMSService.SetAuthenticatedPhoneNumberAsync(phoneNumber);
            return Ok("set auth");

        }

        [HttpPut]
        [Route("{idMessage}/{statusMessage}/callback-update")]
        public async Task<IActionResult> CallbackUpdate(string idMessage, int statusMessage)
        {
            if (string.IsNullOrEmpty(idMessage))
                return BadRequest("Informe o número de ID da mensagem");

            await _clientWhatsMSService.CallbackUpdate(idMessage, statusMessage);
            return Ok("callback ok!");

        }

    }
}
