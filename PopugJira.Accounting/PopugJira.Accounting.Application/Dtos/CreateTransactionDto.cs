using System;

namespace PopugJira.Accounting.Application.Dtos
{
    public record CreateTransactionDto
    {
        public decimal Debit { get; init; }
        public decimal Credit { get; init; }
        public DateTime DateTime { get; init; }
        public string AccountId { get; init; }
        public string Reason { get; init; }
    }
}