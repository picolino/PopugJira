using System;
using LinqToDB.Mapping;
using PopugJira.Analytics.Domain;

namespace PopugJira.Analytics.DataAccessLayer.Entities
{
    [Table("goal_costs")]
    public record GoalCostEntity
    {
        [Column("id"), PrimaryKey]
        public string Id { get; set; }
        
        [Column("title")]
        public string Title { get; set; }
        
        [Column("cost")]
        public decimal? Cost { get; set; }
        
        [Column("complete_datetime")]
        public DateTime? CompleteDateTime { get; set; }

        public GoalCost ToDomain()
        {
            return new GoalCost(Id, Title, Cost, CompleteDateTime);
        }
    }
}