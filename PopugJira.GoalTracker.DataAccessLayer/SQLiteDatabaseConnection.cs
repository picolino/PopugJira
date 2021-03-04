using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;
using PopugJira.GoalTracker.DataAccessLayer.Entities;

namespace PopugJira.GoalTracker.DataAccessLayer
{
    public class SQLiteDatabaseConnection : DataConnection
    {
        public SQLiteDatabaseConnection(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }

        public ITable<GoalEntity> Goals => GetTable<GoalEntity>();
        public ITable<GoalStateEntity> GoalStates => GetTable<GoalStateEntity>();
    }
}