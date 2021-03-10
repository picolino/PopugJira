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

        Task SetState(Guid goalId, GoalState goalState);
        Task<Goal[]> GetAll();
    }
}