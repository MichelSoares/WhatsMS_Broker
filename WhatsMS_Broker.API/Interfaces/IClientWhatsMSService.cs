using WhatsMS_Broker.API.DTOs.Request;
using WhatsMS_Broker.API.DTOs.Response;

namespace WhatsMS_Broker.API.Interfaces
{
    public interface IClientWhatsMSService
    {
        Task<AccountMSStatusResponse?> CheckStatusByPhoneNumberAsync(string phoneNumber);
        Task<int> CheckPhoneNumberExistsAsync(string phoneNumber);
        Task<bool> CheckUptimeGenerateQRCodeAsync(string phoneNumber);
        Task NewInstanceClientNodeAsync(string phoneNumber, UpdateQRCodeDTO newInstanceNode);
        Task ResetQRCodeAsync(string phoneNumber);
        Task NewSessionIdAsync(string phoneNumber, string sessionId);
        Task SetUptimeGenerateQrcodeAsync(string phoneNumber);
        Task SetAuthenticatedPhoneNumberAsync(string phoneNumber);
        Task CallbackUpdate(string idMessage, int statusMessage);
    }
}
