using LinqToDB.Configuration;
using PopugJira.Analytics.DataAccessLayer.Contract;
using Serviced;

namespace PopugJira.Analytics.DataAccessLayer
{
    public class GoalCostWriteDbOperations : GoalCostDbOperations, IGoalCostWriteDbOperations, IScoped<IGoalCostWriteDbOperations>
    {
        public GoalCostWriteDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }
    }
}