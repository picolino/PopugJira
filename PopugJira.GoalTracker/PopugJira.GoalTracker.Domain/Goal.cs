using System;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.Domain
{
    public class Goal
    {
        public Goal(Guid? id,
                    string title,
                    string description, 
                    GoalState state, 
                    Assignee assignee = null)
        {
            Id = id;
            Title = title;
            Description = description;
            State = state;
            Assignee = assignee;
        }

        public Guid? Id { get; }
        public string Title { get; }
        public string Description { get; }
        public GoalState State { get; }
        public Assignee Assignee { get; }
    }
}