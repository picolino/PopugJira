using System;

namespace PopugJira.Domain
{
    public class TopManagementEarnedEntry
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Earned { get; set; }
        public int NegativeEmployeesBalanceCount { get; set; }
    }
}