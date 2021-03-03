using System.Threading.Tasks;
using PopugJira.Domain;

namespace PopugJira.DataAccessLayer.Contract
{
    public interface IGoalStatesDataContext
    {
        Task<GoalState> GetOpenState();
        Task<GoalState> GetClosedState();
    }
}