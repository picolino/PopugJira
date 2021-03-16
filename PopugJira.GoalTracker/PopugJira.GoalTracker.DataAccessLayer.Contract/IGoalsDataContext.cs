using System;
using System.Threading.Tasks;
using PopugJira.GoalTracker.Domain;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.DataAccessLayer.Contract
{
    public interface IGoalsDataContext
    {
        Task Create(Goal goal);
        Task<Goal> Get(string id);
        Task Update(string id, string title, string description);
        Task Delete(string id);

        Task SetState(GoalState goalState, params string[] goalIds);
        Task SetAssignee(string goalId, string assigneeId);
        Task<Goal[]> GetAll();
        Task<string[]> GetIdsByState(GoalState state);
    }
}