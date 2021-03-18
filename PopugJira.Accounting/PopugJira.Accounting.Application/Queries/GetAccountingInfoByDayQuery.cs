using System;
using System.Threading.Tasks;
using PopugJira.Accounting.DataAccessLayer.Contract;
using PopugJira.Accounting.Domain;
using Serviced;

namespace PopugJira.Accounting.Application.Queries
{
    public class GetAccountingInfoByDayQuery : IScoped
    {
        private readonly ITransactionsGetDbOperations transactionsGetDbOperations;

        public GetAccountingInfoByDayQuery(ITransactionsGetDbOperations transactionsGetDbOperations)
        {
            this.transactionsGetDbOperations = transactionsGetDbOperations;
        }

        public async Task<Transaction[]> Query(string accountId, DateTime fromInclusive, DateTime toInclusive)
        {
            return await transactionsGetDbOperations.GetAccountTransactionsInDateRange(accountId, fromInclusive, toInclusive);
        }
    }
}