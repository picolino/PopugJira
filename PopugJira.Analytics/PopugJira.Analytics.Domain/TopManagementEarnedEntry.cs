using System;

namespace PopugJira.Analytics.Domain
{
    public class TopManagementEarnedEntry
    {
        public TopManagementEarnedEntry(string id, DateTime date, decimal earned, int negativeEmployeesBalanceCount)
        {
            Id = id;
            Date = date;
            Earned = earned;
            NegativeEmployeesBalanceCount = negativeEmployeesBalanceCount;
        }

        public string Id { get; }
        public DateTime Date { get; }
        public decimal Earned { get; }
        public int NegativeEmployeesBalanceCount { get; }
    }
}