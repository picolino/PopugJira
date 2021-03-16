using System;

namespace PopugJira.GoalTracker.Application.Dto
{
    public record AssigneeCreateDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}