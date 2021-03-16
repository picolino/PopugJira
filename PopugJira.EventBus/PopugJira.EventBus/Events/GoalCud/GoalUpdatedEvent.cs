namespace PopugJira.EventBus.Events.GoalCud
{
    public class GoalUpdatedEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}