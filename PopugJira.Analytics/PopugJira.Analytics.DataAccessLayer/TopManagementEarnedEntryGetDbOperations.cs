using System;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.Analytics.DataAccessLayer.Contract;
using PopugJira.Analytics.Domain;
using Serviced;

namespace PopugJira.Analytics.DataAccessLayer
{
    public class TopManagementEarnedEntryGetDbOperations : TopManagementEarnedEntryDbOperations, ITopManagementEarnedEntryGetDbOperations, IScoped<ITopManagementEarnedEntryGetDbOperations>
    {
        public TopManagementEarnedEntryGetDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }

        public async Task<TopManagementEarnedEntry> GetByDate(DateTime date)
        {
            var entity = await TopManagementEarnedEntryEntities.Where(o => o.Date == date.Date)
                                                                 .FirstOrDefaultAsync();
            return entity?.ToDomain();
        }
    }
}