using Microsoft.EntityFrameworkCore;
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
    }
}
