using System;

namespace PopugJira.Domain
{
    public record Goal
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public GoalState State { get; set; }
    }
}