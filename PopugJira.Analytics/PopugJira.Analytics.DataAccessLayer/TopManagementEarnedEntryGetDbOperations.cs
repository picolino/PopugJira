using LinqToDB.Configuration;
using PopugJira.Analytics.DataAccessLayer.Contract;
using Serviced;

namespace PopugJira.Analytics.DataAccessLayer
{
    public class TopManagementEarnedEntryGetDbOperations : TopManagementEarnedEntryDbOperations, ITopManagementEarnedEntryGetDbOperations, IScoped<ITopManagementEarnedEntryGetDbOperations>
    {
        public TopManagementEarnedEntryGetDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }
    }
}