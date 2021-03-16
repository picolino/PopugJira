using System;

namespace PopugJira.GoalTracker.Domain
{
    public class Assignee
    {
        public Assignee(string id, string userId, string userName)
        {
            Id = id;
            UserId = userId;
            UserName = userName;
        }

        public string Id { get; }
        public string UserId { get; }
        public string UserName { get; }
    }
}