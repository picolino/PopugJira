using System.Threading.Tasks;
using PopugJira.GoalTracker.Domain;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.DataAccessLayer.Contract
{
    public interface IGoalsGetDbOperations
    {
        Task<Goal> Get(string id);
        Task<Goal[]> Get(params string[] ids);
        Task<Goal[]> GetAll();
        Task<Goal[]> GetByUser(string userId);
        Task<string[]> GetIdsByState(GoalState state);
    }
}