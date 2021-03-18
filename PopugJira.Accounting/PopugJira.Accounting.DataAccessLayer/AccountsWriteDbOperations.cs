using System;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.Accounting.DataAccessLayer.Contract;
using PopugJira.Accounting.DataAccessLayer.Entities;
using PopugJira.Accounting.Domain;
using Serviced;

namespace PopugJira.Accounting.DataAccessLayer
{
    public class AccountsWriteDbOperations : AccountsDbOperations, IAccountsWriteDbOperations, IScoped<IAccountsWriteDbOperations>
    {
        public AccountsWriteDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }

        public async Task Create(Account account)
        {
            await Accounts.InsertAsync(() => new AccountEntity
                                             {
                                                 Id = Guid.NewGuid().ToString(),
                                                 Name = account.Name,
                                                 Balance = account.Balance
                                             });
        }
    }
}