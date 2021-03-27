using LinqToDB.Mapping;
using PopugJira.Accounting.Domain;

namespace PopugJira.Accounting.DataAccessLayer.Entities
{
    [Table("estimated_goals")]
    public record EstimatedGoalEntity
    {
        [Column("id"), PrimaryKey]
        public string Id { get; init; }
        
        [Column("assign_price")]
        public decimal AssignPrice { get; init; }
        
        [Column("complete_price")]
        public decimal CompletePrice { get; init; }

        public EstimatedGoal ToDomain()
        {
            return new EstimatedGoal(Id, AssignPrice, CompletePrice);
        }
    }
}