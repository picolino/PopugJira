using System;

namespace PopugJira.EventBus.Events.BusinessEvents
{
    public class GoalCompletedEventV1
    {
        public string Id { get; set; }
        public string AssigneeId { get; set; }
        public decimal CompletePrice { get; set; }
        public DateTime CompleteDateTime { get; set; }
    }
}