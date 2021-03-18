using System;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.Accounting.DataAccessLayer.Contract;
using PopugJira.Accounting.DataAccessLayer.Entities;
using PopugJira.Accounting.Domain;

namespace PopugJira.Accounting.DataAccessLayer
{
    public class TransactionsWriteDbOperations : TransactionsDbOperations, ITransactionsWriteDbOperations
    {
        public TransactionsWriteDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }


        public async Task Create(Transaction transaction)
        {
            await Transactions.InsertAsync(() => new TransactionEntity
                                                 {
                                                     Id = Guid.NewGuid().ToString(),
                                                     AccountId = transaction.Account.Id,
                                                     DateTime = transaction.DateTime,
                                                     Debit = transaction.Debit,
                                                     Credit = transaction.Credit,
                                                     Reason = transaction.Reason
                                                 });
        }
    }
}