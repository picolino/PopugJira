using System;

namespace PopugJira.EventBus.Events.BusinessEvents
{
    public class GoalCompletedEvent
    {
        public string Id { get; set; }
        public string AssigneeId { get; set; }
        public decimal CompletePrice { get; set; }
        public DateTime CompleteDateTime { get; set; }
    }
}