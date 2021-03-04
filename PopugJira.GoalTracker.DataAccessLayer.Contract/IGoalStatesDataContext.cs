using System.Threading.Tasks;
using PopugJira.GoalTracker.Domain;

namespace PopugJira.GoalTracker.DataAccessLayer.Contract
{
    public interface IGoalStatesDataContext
    {
        Task<GoalState> GetOpenState();
        Task<GoalState> GetClosedState();
    }
}