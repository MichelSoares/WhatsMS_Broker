using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsMS_Broker.Domain.Entidades;

namespace WhatsMS_Broker.Data.Interfaces
{
    public interface IAccountMSRepository
    {
        Task<IEnumerable<AccountMS>> GetAllAccountsAsync();
        Task AddAsync(AccountMS account);
    }
}
