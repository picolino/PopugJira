using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.GoalTracker.DataAccessLayer.Entities;

namespace PopugJira.GoalTracker.DataAccessLayer
{
    public class AssigneeDbOperations : SQLiteDatabaseConnection
    {
        protected ITable<AssigneeEntity> Assignees => GetTable<AssigneeEntity>();

        public AssigneeDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }
    }
}