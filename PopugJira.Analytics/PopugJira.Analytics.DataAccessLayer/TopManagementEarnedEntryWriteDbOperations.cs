using System;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.Analytics.DataAccessLayer.Contract;
using PopugJira.Analytics.DataAccessLayer.Entities;
using Serviced;

namespace PopugJira.Analytics.DataAccessLayer
{
    public class TopManagementEarnedEntryWriteDbOperations : TopManagementEarnedEntryDbOperations, ITopManagementEarnedEntryWriteDbOperations, IScoped<ITopManagementEarnedEntryWriteDbOperations>
    {
        public TopManagementEarnedEntryWriteDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }

        public async Task AddEarnedWithNegativeEmployeesCountIncrementOrCreateNew(DateTime date, decimal addEarnedAmount)
        {
            await TopManagementEarnedEntryEntities
                .InsertOrUpdateAsync(() => new TopManagementEarnedEntryEntity
                                           {
                                               Id = Guid.NewGuid().ToString(),
                                               Date = date,
                                               Earned = addEarnedAmount,
                                               NegativeEmployeesBalanceCount = 1
                                           },
                                     e => new TopManagementEarnedEntryEntity
                                          {
                                              Earned = e.Earned + addEarnedAmount,
                                              NegativeEmployeesBalanceCount = e.NegativeEmployeesBalanceCount + 1
                                          },
                                     () => new TopManagementEarnedEntryEntity
                                           {
                                               Date = date.Date
                                           });
        }
    }
}