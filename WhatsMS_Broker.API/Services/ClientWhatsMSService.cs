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
    }
}
