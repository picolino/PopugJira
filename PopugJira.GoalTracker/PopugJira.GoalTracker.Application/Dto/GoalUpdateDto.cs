using System;

namespace PopugJira.GoalTracker.Application.Dto
{
    public record GoalUpdateDto : GoalCreateDto
    {
        public Guid Id { get; set; }
    }
}