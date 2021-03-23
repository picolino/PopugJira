using System;
using PopugJira.Accounting.Domain;

namespace PopugJira.Accounting.Application.Dtos
{
    public record AccountingInfoItemQueryResult
    {
        public AccountingInfoItemQueryResult(Transaction transaction)
        {
            Reason = transaction.Reason;
            Amount = transaction.Debit + transaction.Credit;
            DateTime = transaction.DateTime;
        }

        public DateTime DateTime { get; }
        public decimal Amount { get; }
        public string Reason { get; }
    }
}