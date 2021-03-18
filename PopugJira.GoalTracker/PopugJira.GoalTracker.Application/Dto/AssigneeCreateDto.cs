using System;

namespace PopugJira.GoalTracker.Application.Dto
{
    public record AssigneeCreateDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}