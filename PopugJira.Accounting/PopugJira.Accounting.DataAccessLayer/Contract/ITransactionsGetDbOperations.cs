using System;
using System.Threading.Tasks;
using PopugJira.Accounting.Domain;

namespace PopugJira.Accounting.DataAccessLayer.Contract
{
    public interface ITransactionsGetDbOperations
    {
        Task<Transaction[]> GetAllTransactionsInDateRange(DateTime fromInclusive, DateTime toInclusive);
        Task<Transaction[]> GetAccountTransactionsInDateRange(string accountId, DateTime fromInclusive, DateTime toInclusive);
    }
}