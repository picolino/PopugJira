namespace PopugJira.EventBus.Events.GoalCud
{
    public class GoalUpdatedEventV1
    {
        public string Id { get; set; }
        public GoalUpdatedEventV1GoalPart GoalPart { get; set; }
        public GoalUpdatedEventV1PricePart EstimatePart { get; set; }
    }

    public class GoalUpdatedEventV1GoalPart
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
    
    public class GoalUpdatedEventV1PricePart
    {
        public decimal AssignPrice { get; set; }
        public decimal CompletePrice { get; set; }
    }
}