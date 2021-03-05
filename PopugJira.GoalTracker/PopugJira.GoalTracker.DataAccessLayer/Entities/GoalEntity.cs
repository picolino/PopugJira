using LinqToDB.Mapping;
using PopugJira.GoalTracker.Domain;
using PopugJira.GoalTracker.Domain.Definitions;

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
        
        [Column("state")]
        public GoalState State { get; init; }

        public Goal ToDomain()
        {
            return new (Id, Description, State);
        }
    }
}