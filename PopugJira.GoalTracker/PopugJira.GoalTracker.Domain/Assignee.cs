using System;

namespace PopugJira.GoalTracker.Domain
{
    public class Assignee
    {
        public Assignee(string id, string userName)
        {
            Id = id;
            UserName = userName;
        }

        public string Id { get; }
        public string UserName { get; }
    }
}