using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.Accounting.DataAccessLayer.Contract;
using PopugJira.Accounting.Domain;
using Serviced;

namespace PopugJira.Accounting.DataAccessLayer
{
    public class AccountsGetDbOperations : AccountsDbOperations, IAccountsGetDbOperations, IScoped<IAccountsGetDbOperations>
    {
        public AccountsGetDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }


        public async Task<Account> Get(string id)
        {
            var entity = await Accounts.FirstOrDefaultAsync(o => o.Id == id);
            return entity.ToDomain();
        }
    }
}