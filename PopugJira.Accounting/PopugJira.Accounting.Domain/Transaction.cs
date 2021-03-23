using System;

namespace PopugJira.Accounting.Domain
{
    public class Transaction
    {
        public Transaction(string id, Account account, DateTime dateTime, decimal debit, decimal credit, string reason)
        {
            Id = id;
            Account = account;
            DateTime = dateTime;
            Debit = debit;
            Credit = credit;
            Reason = reason;
        }

        public string Id { get; }
        public Account Account { get; }
        public DateTime DateTime { get; }
        public decimal Debit { get; }
        public decimal Credit { get; }
        public string Reason { get; }
    }
}