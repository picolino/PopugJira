using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.Domain
{
    public class Goal
    {
        public Goal(int? id, string description, GoalState state)
        {
            Id = id;
            Description = description;
            State = state;
        }
        
        public int? Id { get; }
        public string Description { get; }
        public GoalState State { get; }
    }
}