using System.Threading.Tasks;
using PopugJira.GoalTracker.Domain;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.DataAccessLayer.Contract
{
    public interface IGoalsWriteDbOperations
    {
        Task Create(Goal goal);
        Task Update(string id, string title, string description);
        Task Delete(string id);

        Task SetState(GoalState goalState, params string[] goalIds);
        Task SetAssignee(string goalId, string assigneeId);
    }
}