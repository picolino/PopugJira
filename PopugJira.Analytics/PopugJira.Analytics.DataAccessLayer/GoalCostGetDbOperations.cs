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
    public class GoalCostGetDbOperations : GoalCostDbOperations, IGoalCostGetDbOperations, IScoped<IGoalCostGetDbOperations>
    {
        public GoalCostGetDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }

        public async Task<GoalCost> GetMostCostlyForPeriod(DateTime from, DateTime to)
        {
            var entity = await GoalCosts.Where(o => o.CompleteDateTime.Between(from, to))
                                        .OrderByDescending(o => o.Cost)
                                        .FirstOrDefaultAsync();
            return entity?.ToDomain();
        }
    }
}