using System;
using System.Threading.Tasks;
using PopugJira.Analytics.Domain;

namespace PopugJira.Analytics.DataAccessLayer.Contract
{
    public interface IGoalCostWriteDbOperations
    {
        Task Create(GoalCost goalCost);
        Task Complete(string goalId, DateTime completeDateTime);
        Task Update(string goalId, string title);
        Task SetCost(string goalId, decimal cost);
    }
}