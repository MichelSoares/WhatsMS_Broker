using Microsoft.EntityFrameworkCore;
using WhatsMS_Broker.API.DTOs.Request;
using WhatsMS_Broker.API.DTOs.Response;
using WhatsMS_Broker.API.Interfaces;
using WhatsMS_Broker.Data.Context;

namespace WhatsMS_Broker.API.Services
{
    public class ClientWhatsMSService : IClientWhatsMSService
    {
        private readonly BrokerDbContext _brokerDbContext;

        public ClientWhatsMSService(BrokerDbContext brokerDbContext)
        {
            _brokerDbContext = brokerDbContext;
        }

        public async Task<AccountMSStatusDTO?> CheckStatusByPhoneNumberAsync(string phoneNumber)
        {
            return 
                await _brokerDbContext.Accounts.Where(a => a.PhoneNumber == phoneNumber)
                .Select(a => new AccountMSStatusDTO
                {
                    client_session_id = a.ClientSessionID,
                    phone_number = a.PhoneNumber,
                    is_active = a.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<int> CheckPhoneNumberExists(string phoneNumber)
        {
            return await _brokerDbContext.Accounts
                .CountAsync(a => a.PhoneNumber == phoneNumber);
        }

        public async Task<bool> CheckUptimeGenerateQRCode(string phoneNumber)
        {
            return await _brokerDbContext.Accounts.AnyAsync(a => a.PhoneNumber == phoneNumber && a.UpdatedAt > DateTime.UtcNow.AddMinutes(-1));
        }

        public async Task NewInstanceClientNode(string phoneNumber, UpdateQRCodeDTO newInstanceNode)
        {
            var account = await _brokerDbContext.Accounts
                .FirstOrDefaultAsync(a => a.PhoneNumber == phoneNumber);

            if (account == null)
                throw new Exception("Telefone não encontrado.");

            account.QrCodeBase64 = newInstanceNode.QRCode;
            account.PortRun = newInstanceNode.Port.ToString();
            account.UpdatedAt = DateTime.UtcNow;

            await _brokerDbContext.SaveChangesAsync();
        }

        public async Task ResetQRCode(string phoneNumber)
        {
            var account = await _brokerDbContext.Accounts
                .FirstOrDefaultAsync(a => a.PhoneNumber == phoneNumber);

            if (account == null)
                throw new Exception("Telefone não encontrado.");

            account.QrCodeBase64 = string.Empty;
            account.ClientSessionID = string.Empty;
            account.IsActive = false;

            await _brokerDbContext.SaveChangesAsync();
        }

        public async Task NewSessionId(string phoneNumber, string sessionId)
        {
            var account = await _brokerDbContext.Accounts
               .FirstOrDefaultAsync(a => a.PhoneNumber == phoneNumber);

            if (account == null)
                throw new Exception("Telefone não encontrado.");

            account.ClientSessionID = sessionId;

            await _brokerDbContext.SaveChangesAsync();
        }

        public async Task SetUptimeGenerateQrcode(string phoneNumber)
        {
            var account = await _brokerDbContext.Accounts
               .FirstOrDefaultAsync(a => a.PhoneNumber == phoneNumber);

            if (account == null)
                throw new Exception("Telefone não encontrado.");

            account.UpdatedAt = DateTime.UtcNow;

            await _brokerDbContext.SaveChangesAsync();
        }

        public async Task SetAuthenticatedPhoneNumber(string phoneNumber)
        {
            var account = await _brokerDbContext.Accounts
              .FirstOrDefaultAsync(a => a.PhoneNumber == phoneNumber);

            if (account == null)
                throw new Exception("Telefone não encontrado.");

            account.IsActive = true;

            await _brokerDbContext.SaveChangesAsync();
        }
    }
}
