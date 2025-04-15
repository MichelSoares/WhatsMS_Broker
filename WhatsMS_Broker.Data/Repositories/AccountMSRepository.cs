using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsMS_Broker.Data.Context;
using WhatsMS_Broker.Data.Interfaces;
using WhatsMS_Broker.Domain.Entidades;

namespace WhatsMS_Broker.Data.Repositories
{
    public class AccountMSRepository : IAccountMSRepository
    {
        private readonly BrokerDbContext _context;

        public AccountMSRepository(BrokerDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(AccountMS account)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccountMS>> GetAllAccountsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
