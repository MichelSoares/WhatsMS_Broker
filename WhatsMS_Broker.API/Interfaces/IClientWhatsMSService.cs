using WhatsMS_Broker.API.DTOs.Response;

namespace WhatsMS_Broker.API.Interfaces
{
    public interface IClientWhatsMSService
    {
        Task<AccountMSStatusDTO?> CheckStatusByPhoneNumberAsync(string phoneNumber);
    }
}
