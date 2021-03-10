using System;
using LinqToDB.Mapping;
using PopugJira.GoalTracker.Domain;

namespace PopugJira.GoalTracker.DataAccessLayer.Entities
{
    [Table("assignees")]
    public record AssigneeEntity
    {
        [PrimaryKey]
        [Column("id")]
        public Guid Id { get; init; }
        
        [Column("user_id")]
        public Guid UserId { get; init; }
        
        [Column("user_name")]
        public string UserName { get; init; }

        public Assignee ToDomain()
        {
            return new (Id, UserId, UserName);
        }
    }
}