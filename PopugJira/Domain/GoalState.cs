namespace PopugJira.Domain
{
    public record GoalState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SystemGoalState SystemState { get; set; }
    }
}