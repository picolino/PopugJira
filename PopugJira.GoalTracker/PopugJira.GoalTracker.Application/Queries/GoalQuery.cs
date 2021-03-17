using System;
using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain;

namespace PopugJira.GoalTracker.Application.Queries
{
    public class GoalQuery : IQuery
    {
        private readonly IGoalsGetDbOperations goalsGetDbOperations;

        public GoalQuery(IGoalsGetDbOperations goalsGetDbOperations)
        {
            this.goalsGetDbOperations = goalsGetDbOperations;
        }
        
        public async Task<Goal> Query(string id)
        {
            return await goalsGetDbOperations.Get(id);
        }
    }
}