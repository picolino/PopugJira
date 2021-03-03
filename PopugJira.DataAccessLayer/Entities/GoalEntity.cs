using LinqToDB.Mapping;
using PopugJira.Domain;

namespace PopugJira.DataAccessLayer.Entities
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
        
        [Association(ThisKey="goal_state_id", OtherKey="id")]
        public GoalStateEntity GoalState { get; init; }

        public Goal ToDomain()
        {
            return new (Id, Description, GoalState.ToDomain());
        }
    }
}