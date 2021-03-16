using System;

namespace PopugJira.EventBus.Events.BusinessEvents
{
    public class GoalAssignedEvent
    {
        public string Id { get; set; }
        public string AssigneeId { get; set; }
        public decimal AssignPrice { get; set; }
        public DateTime AssignDateTime { get; set; }
    }
}