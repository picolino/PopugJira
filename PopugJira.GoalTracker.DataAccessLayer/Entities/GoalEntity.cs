using LinqToDB.Mapping;
using PopugJira.GoalTracker.Domain;

namespace PopugJira.GoalTracker.DataAccessLayer.Entities
{
    [Table("goals")]
    public record GoalEntity
    {
        [PrimaryKey]
        [Column("id")]
        public int Id { get; init; }
        
        [Column("description")]
        public string Description { get; init; }
        
        [Column("goal_state_id")]
        public int GoalStateId { get; init; }
        
        [Association(ThisKey=nameof(GoalStateId), OtherKey=nameof(GoalStateEntity.Id))]
        public GoalStateEntity GoalState { get; init; }

        public Goal ToDomain()
        {
            return new (Id, Description, GoalState.ToDomain());
        }
    }
}