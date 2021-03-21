using System;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.Domain
{
    public class Goal
    {
        public Goal(string id,
                    string title,
                    string description, 
                    decimal assignPrice,
                    decimal completePrice,
                    GoalState state, 
                    Assignee assignee = null)
        {
            Id = id;
            Title = title;
            Description = description;
            AssignPrice = assignPrice;
            CompletePrice = completePrice;
            State = state;
            Assignee = assignee;
        }

        public string Id { get; }
        public string Title { get; }
        public string Description { get; }
        public decimal AssignPrice { get; }
        public decimal CompletePrice { get; }
        public GoalState State { get; }
        public Assignee Assignee { get; }
    }
}