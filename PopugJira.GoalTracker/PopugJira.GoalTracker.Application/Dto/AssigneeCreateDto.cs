using System;

namespace PopugJira.GoalTracker.Application.Dto
{
    public record AssigneeCreateDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}