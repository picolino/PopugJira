using System;

namespace PopugJira.Domain
{
    public class GoalCost
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public decimal Cost { get; set; }
        public DateTime? CompletedDateTime { get; set; }
    }
}