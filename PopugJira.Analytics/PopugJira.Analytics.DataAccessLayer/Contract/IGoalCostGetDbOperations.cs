using System;
using System.Threading.Tasks;
using PopugJira.Analytics.Domain;

namespace PopugJira.Analytics.DataAccessLayer.Contract
{
    public interface IGoalCostGetDbOperations
    {
        Task<GoalCost> GetMostCostlyForPeriod(DateTime from, DateTime to);
    }
}