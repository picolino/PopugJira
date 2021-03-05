namespace PopugJira.GoalTracker.Application.Dto
{
    public record GoalUpdateDto : GoalCreateDto
    {
        public int Id { get; set; }
    }
}