using LinqToDB.Configuration;
using LinqToDB.Data;

namespace PopugJira.Analytics.DataAccessLayer
{
    public class SQLiteDatabaseConnection : DataConnection
    {
        public SQLiteDatabaseConnection(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }
    }
}