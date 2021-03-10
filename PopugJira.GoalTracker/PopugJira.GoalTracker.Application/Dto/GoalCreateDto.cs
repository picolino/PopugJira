namespace PopugJira.GoalTracker.Application.Dto
{
    public record GoalCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}