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
        public string Id { get; init; }
        
        [Column("name")]
        public string Name { get; init; }

        public Assignee ToDomain()
        {
            return new (Id, Name);
        }
    }
}