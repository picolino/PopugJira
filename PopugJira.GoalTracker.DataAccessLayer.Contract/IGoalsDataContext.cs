using System.Threading.Tasks;
using PopugJira.GoalTracker.Domain;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.DataAccessLayer.Contract
{
    public interface IGoalsDataContext
    {
        Task<int> Create(Goal goal);
        Task<Goal> Get(int id);
        Task Update(int id, string description);
        Task Delete(int id);

        Task SetState(int goalId, GoalState goalState);
        Task<Goal[]> GetAll();
    }
}