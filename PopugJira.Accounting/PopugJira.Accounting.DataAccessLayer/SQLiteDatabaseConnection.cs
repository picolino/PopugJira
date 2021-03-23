using LinqToDB.Configuration;
using LinqToDB.Data;

namespace PopugJira.Accounting.DataAccessLayer
{
    public class SQLiteDatabaseConnection : DataConnection
    {
        public SQLiteDatabaseConnection(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }
    }
}