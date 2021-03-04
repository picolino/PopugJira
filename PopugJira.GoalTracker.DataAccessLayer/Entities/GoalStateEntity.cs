using LinqToDB.Mapping;
using PopugJira.GoalTracker.Domain;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.DataAccessLayer.Entities
{
    [Table("goal_states")]
    public record GoalStateEntity
    {
        [PrimaryKey]
        [Column("id")]
        public int Id { get; init; }
        
        [Column("name")]
        public string Name { get; init; }
        
        [Column("is_system")]
        public bool IsSystem { get; init; }

        public GoalState ToDomain()
        {
            var systemGoalState = this switch
            {
                {IsSystem: true, Name: nameof(SystemGoalState.Open)} => SystemGoalState.Open,
                {IsSystem: true, Name: nameof(SystemGoalState.Closed)} => SystemGoalState.Closed,
                _ => SystemGoalState.Custom
            };
            
            return new(Id, Name, systemGoalState);
        }
    }
}