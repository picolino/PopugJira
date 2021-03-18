using System;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.Accounting.DataAccessLayer.Contract;
using PopugJira.Accounting.Domain;
using Serviced;

namespace PopugJira.Accounting.DataAccessLayer
{
    public class TransactionsGetDbOperations : TransactionsDbOperations, ITransactionsGetDbOperations, IScoped<ITransactionsGetDbOperations>
    {
        public TransactionsGetDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }

        public async Task<Transaction[]> GetAllTransactionsInDateRange(DateTime fromInclusive, DateTime toInclusive)
        {
            var entities = await Transactions.LoadWith(o => o.Account)
                                             .Where(o => o.DateTime.Between(fromInclusive, toInclusive))
                                             .ToArrayAsync();
            return entities.Select(o => o.ToDomain()).ToArray();
        }

        public async Task<Transaction[]> GetAccountTransactionsInDateRange(string accountId, DateTime fromInclusive, DateTime toInclusive)
        {
            var entities = await Transactions.LoadWith(o => o.Account)
                                             .Where(o => o.AccountId == accountId)
                                             .Where(o => o.DateTime.Between(fromInclusive, toInclusive))
                                             .ToArrayAsync();
            return entities.Select(o => o.ToDomain()).ToArray();
        }
    }
}