using System;

namespace PopugJira.GoalTracker.Application.Dto
{
    public record GoalUpdateDto : GoalCreateDto
    {
        public string Id { get; set; }
    }
}