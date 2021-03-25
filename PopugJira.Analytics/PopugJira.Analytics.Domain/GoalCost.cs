using System;

namespace PopugJira.Analytics.Domain
{
    public class GoalCost
    {
        public GoalCost(string id, string title, decimal? cost, DateTime? completedDateTime)
        {
            Id = id;
            Title = title;
            Cost = cost;
            CompletedDateTime = completedDateTime;
        }
        
        public string Id { get; }
        public string Title { get; }
        public decimal? Cost { get; }
        public DateTime? CompletedDateTime { get; }
    }
}