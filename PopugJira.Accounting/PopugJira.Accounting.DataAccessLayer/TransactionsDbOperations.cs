using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.Accounting.DataAccessLayer.Entities;

namespace PopugJira.Accounting.DataAccessLayer
{
    public class TransactionsDbOperations : SQLiteDatabaseConnection
    {
        protected ITable<TransactionEntity> Transactions => GetTable<TransactionEntity>();
        
        public TransactionsDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }
    }
}