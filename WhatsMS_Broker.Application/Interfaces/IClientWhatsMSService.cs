using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsMS_Broker.Application.DTOs.Responses;

namespace WhatsMS_Broker.Application.Interfaces
{
    public interface IClientWhatsMSService
    {
        Task<AccountMSStatusResponse?> CheckStatusByPhoneNumberAsync(string phoneNumber);
        Task<int> CheckPhoneNumberExistsAsync(string phoneNumber);
        Task<bool> CheckUptimeGenerateQRCodeAsync(string phoneNumber);
        //Task NewInstanceClientNodeAsync(string phoneNumber, UpdateQRCodeDTO newInstanceNode);
        Task ResetQRCodeAsync(string phoneNumber);
        Task NewSessionIdAsync(string phoneNumber, string sessionId);
        Task SetUptimeGenerateQrcodeAsync(string phoneNumber);
        Task SetAuthenticatedPhoneNumberAsync(string phoneNumber);
        Task CallbackUpdate(string idMessage, int statusMessage);
    }
}
