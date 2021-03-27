using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.Analytics.DataAccessLayer.Entities;

namespace PopugJira.Analytics.DataAccessLayer
{
    public class TopManagementEarnedEntryDbOperations : SQLiteDatabaseConnection
    {
        protected ITable<TopManagementEarnedEntryEntity> TopManagementEarnedEntryEntities => GetTable<TopManagementEarnedEntryEntity>();
        
        public TopManagementEarnedEntryDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }
    }
}