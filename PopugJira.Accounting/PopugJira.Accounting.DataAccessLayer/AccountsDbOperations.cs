using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.Accounting.DataAccessLayer.Entities;

namespace PopugJira.Accounting.DataAccessLayer
{
    public class AccountsDbOperations : SQLiteDatabaseConnection
    {
        protected ITable<AccountEntity> Accounts => GetTable<AccountEntity>();
        
        public AccountsDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }
    }
}