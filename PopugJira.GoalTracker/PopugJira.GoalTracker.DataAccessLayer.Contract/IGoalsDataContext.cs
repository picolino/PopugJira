using System;
using System.Threading.Tasks;
using PopugJira.GoalTracker.Domain;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.DataAccessLayer.Contract
{
    public interface IGoalsDataContext
    {
        Task Create(Goal goal);
        Task<Goal> Get(Guid id);
        Task Update(Guid id, string title, string description);
        Task Delete(Guid id);

        Task SetState(GoalState goalState, params Guid[] goalIds);
        Task SetAssignee(Guid goalId, Guid assigneeId);
        Task<Goal[]> GetAll();
        Task<Guid[]> GetIdsByState(GoalState state);
    }
}