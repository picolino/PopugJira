namespace PopugJira.EventBus.Events.GoalCud
{
    public class GoalCreatedEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal AssignPrice { get; set; } 
        public decimal CompletePrice { get; set; } 
    }
}