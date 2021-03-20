using System;
using System.Threading.Tasks;
using PopugJira.Analytics.DataAccessLayer.Contract;
using PopugJira.Analytics.Domain;
using Serviced;

namespace PopugJira.Analytics.Application.Queries
{
    public class MostCostlyGoalForPeriodQuery : IScoped
    {
        private readonly IGoalCostGetDbOperations goalCostGetDbOperations;

        public MostCostlyGoalForPeriodQuery(IGoalCostGetDbOperations goalCostGetDbOperations)
        {
            this.goalCostGetDbOperations = goalCostGetDbOperations;
        }
        
        public async Task<GoalCost> Query(DateTime from, DateTime to)
        {
            return await goalCostGetDbOperations.GetMostCostlyForPeriod(from, to);
        }
    }
}