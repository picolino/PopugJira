using System.Threading.Tasks;

namespace PopugJira.GoalTracker.DataAccessLayer.Contract
{
    public interface IGoalsConfigGetDbOperations
    {
        Task<decimal> GetAssignGoalPrice();
        Task<decimal> GetCompleteGoalPrice();
    }
}