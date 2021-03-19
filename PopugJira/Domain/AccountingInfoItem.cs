using System;

namespace PopugJira.Domain
{
    public record AccountingInfoItem
    {
        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
    }
}