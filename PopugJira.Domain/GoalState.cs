using PopugJira.Domain.Definitions;

namespace PopugJira.Domain
{
    public class GoalState
    {
        public GoalState(int id, string name, SystemGoalState systemState)
        {
            Id = id;
            Name = name;
            SystemState = systemState;
        }

        public int Id { get; }
        public string Name { get; }
        public SystemGoalState SystemState { get; }
    }
}