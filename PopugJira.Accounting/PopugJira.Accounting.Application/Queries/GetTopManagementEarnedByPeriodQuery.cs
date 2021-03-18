using System;
using System.Linq;
using System.Threading.Tasks;
using PopugJira.Accounting.DataAccessLayer.Contract;

namespace PopugJira.Accounting.Application.Queries
{
    public class GetTopManagementEarnedByPeriodQuery
    {
        private readonly ITransactionsGetDbOperations transactionsGetDbOperations;

        public GetTopManagementEarnedByPeriodQuery(ITransactionsGetDbOperations transactionsGetDbOperations)
        {
            this.transactionsGetDbOperations = transactionsGetDbOperations;
        }

        public async Task<decimal> Query(DateTime fromInclusive, DateTime toInclusive)
        {
            var transactions = await transactionsGetDbOperations.GetAllTransactionsInDateRange(fromInclusive, toInclusive);
            var amount = transactions.Select(o => o.Debit + o.Credit).Sum() * -1;
            return amount;
        }
    }
}