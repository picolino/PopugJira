using System;
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
        public string Id { get; init; }
        
        [Column("title")]
        public string Title { get; init; }
        
        [Column("description")]
        public string Description { get; init; }
        
        [Column("assign_price")]
        public decimal AssignPrice { get; init; }
        
        [Column("complete_price")]
        public decimal CompletePrice { get; init; }
        
        [Column("state")]
        public GoalState State { get; init; }
        
        [Column("assignee_id")]
        public string AssigneeId { get; init; }
        
        [Association(ThisKey = nameof(AssigneeId), OtherKey = nameof(AssigneeEntity.Id))]
        public AssigneeEntity Assignee { get; init; }

        public Goal ToDomain()
        {
            return new (Id, Title, Description, AssignPrice, CompletePrice, State, Assignee?.ToDomain());
        }
    }
}