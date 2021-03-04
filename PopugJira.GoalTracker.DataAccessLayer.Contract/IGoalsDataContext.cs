using System.Threading.Tasks;
using PopugJira.GoalTracker.Domain;

namespace PopugJira.GoalTracker.DataAccessLayer.Contract
{
    public interface IGoalsDataContext
    {
        Task<int> Create(Goal goal);
        Task<Goal> Get(int id);
        Task Update(int id, string description);
        Task Delete(int id);

        Task SetState(int goalId, int goalStateId);
        Task<Goal[]> GetAll();
    }
}