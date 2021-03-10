using System;

namespace PopugJira.GoalTracker.Domain
{
    public class Assignee
    {
        public Assignee(Guid? id, Guid userId, string userName)
        {
            Id = id;
            UserId = userId;
            UserName = userName;
        }

        public Guid? Id { get; }
        public Guid UserId { get; }
        public string UserName { get; }
    }
}