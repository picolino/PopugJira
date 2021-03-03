using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;
using PopugJira.DataAccessLayer.Entities;

namespace PopugJira.DataAccessLayer
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