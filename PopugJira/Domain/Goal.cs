namespace PopugJira.Domain
{
    public record Goal
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public GoalState State { get; set; } = new();
    }
}