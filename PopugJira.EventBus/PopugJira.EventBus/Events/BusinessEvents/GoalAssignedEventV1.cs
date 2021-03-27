using System;

namespace PopugJira.EventBus.Events.BusinessEvents
{
    public class GoalAssignedEventV1
    {
        public string Id { get; set; }
        public string AssigneeId { get; set; }
        public DateTime AssignDateTime { get; set; }
    }
}