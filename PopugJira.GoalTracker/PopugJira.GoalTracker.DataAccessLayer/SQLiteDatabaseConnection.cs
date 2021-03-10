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
    }
}