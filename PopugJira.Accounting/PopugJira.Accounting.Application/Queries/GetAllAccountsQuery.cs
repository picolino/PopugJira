using System.Collections.Generic;
using System.Threading.Tasks;
using PopugJira.Accounting.DataAccessLayer.Contract;
using PopugJira.Accounting.Domain;
using Serviced;

namespace PopugJira.Accounting.Application.Queries
{
    public class GetAllAccountsQuery : IScoped
    {
        private readonly IAccountsGetDbOperations accountsGetDbOperations;

        public GetAllAccountsQuery(IAccountsGetDbOperations accountsGetDbOperations)
        {
            this.accountsGetDbOperations = accountsGetDbOperations;
        }
        
        public async Task<Account[]> Query()
        {
            return await accountsGetDbOperations.All();
        }
    }
}