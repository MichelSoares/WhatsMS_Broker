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

        public async Task<AccountMSStatusResponse?> CheckStatusByPhoneNumberAsync(string phoneNumber)
        {
            return 
                await _brokerDbContext.Accounts.Where(a => a.PhoneNumber == phoneNumber)
                .Select(a => new AccountMSStatusResponse
                {
                    id = a.Id,
                    auth_token = a.AuthToken,
                    client_session_id = a.ClientSessionID,
                    phone_number = a.PhoneNumber,
                    is_active = a.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<int> CheckPhoneNumberExistsAsync(string phoneNumber)
        {
            return await _brokerDbContext.Accounts
                .CountAsync(a => a.PhoneNumber == phoneNumber);
        }

        public async Task<bool> CheckUptimeGenerateQRCodeAsync(string phoneNumber)
        {
            return await _brokerDbContext.Accounts.AnyAsync(a => a.PhoneNumber == phoneNumber && a.UpdatedAt > DateTime.UtcNow.AddMinutes(-1));
        }

        public async Task NewInstanceClientNodeAsync(string phoneNumber, UpdateQRCodeDTO newInstanceNode)
        {
            var account = await _brokerDbContext.Accounts
                .FirstOrDefaultAsync(a => a.PhoneNumber == phoneNumber);

            if (account == null)
                throw new Exception("Telefone não encontrado.");

            account.QrCodeBase64 = newInstanceNode.QRCode;
            account.PortRun = newInstanceNode.Port;
            account.UpdatedAt = DateTime.UtcNow;

            await _brokerDbContext.SaveChangesAsync();
        }

        public async Task ResetQRCodeAsync(string phoneNumber)
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

        public async Task NewSessionIdAsync(string phoneNumber, string sessionId)
        {
            var account = await _brokerDbContext.Accounts
               .FirstOrDefaultAsync(a => a.PhoneNumber == phoneNumber);

            if (account == null)
                throw new Exception("Telefone não encontrado.");

            account.ClientSessionID = sessionId;

            await _brokerDbContext.SaveChangesAsync();
        }

        public async Task SetUptimeGenerateQrcodeAsync(string phoneNumber)
        {
            var account = await _brokerDbContext.Accounts
               .FirstOrDefaultAsync(a => a.PhoneNumber == phoneNumber);

            if (account == null)
                throw new Exception("Telefone não encontrado.");

            account.UpdatedAt = DateTime.UtcNow;

            await _brokerDbContext.SaveChangesAsync();
        }

        public async Task SetAuthenticatedPhoneNumberAsync(string phoneNumber)
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
