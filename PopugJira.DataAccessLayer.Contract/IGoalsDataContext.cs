using System;
using System.Threading.Tasks;
using PopugJira.Domain;

namespace PopugJira.DataAccessLayer.Contract
{
    public interface IGoalsDataContext
    {
        Task<int> Create(Goal goal);
        Task<Goal> Get(int id);
        Task Delete(int id);

        Task SetState(int goalId, int goalStateId);
    }
}