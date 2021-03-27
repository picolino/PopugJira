using System.Threading.Tasks;

namespace PopugJira.Accounting.DataAccessLayer.Contract
{
    public interface IGoalsConfigGetDbOperations
    {
        Task<decimal> GetAssignGoalPrice();
        Task<decimal> GetCompleteGoalPrice();
    }
}