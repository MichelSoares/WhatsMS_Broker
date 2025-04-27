using WhatsMS_Broker.API.DTOs.Request;
using WhatsMS_Broker.API.DTOs.Response;

namespace WhatsMS_Broker.API.Interfaces
{
    public interface IClientWhatsMSService
    {
        Task<AccountMSStatusDTO?> CheckStatusByPhoneNumberAsync(string phoneNumber);
        Task<int> CheckPhoneNumberExists(string phoneNumber);
        Task<bool> CheckUptimeGenerateQRCode(string phoneNumber);
        Task NewInstanceClientNode(string phoneNumber, UpdateQRCodeDTO newInstanceNode);
        Task ResetQRCode(string phoneNumber);
        Task NewSessionId(string phoneNumber, string sessionId);
        Task SetUptimeGenerateQrcode(string phoneNumber);
        Task SetAuthenticatedPhoneNumber(string phoneNumber);
    }
}
