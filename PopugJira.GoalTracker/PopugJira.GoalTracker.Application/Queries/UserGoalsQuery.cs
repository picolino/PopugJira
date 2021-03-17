using System;
using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain;

namespace PopugJira.GoalTracker.Application.Queries
{
    public class UserGoalsQuery : IQuery
    {
        private readonly IGoalsGetDbOperations goalsGetDbOperations;

        public UserGoalsQuery(IGoalsGetDbOperations goalsGetDbOperations)
        {
            this.goalsGetDbOperations = goalsGetDbOperations;
        }

        public async Task<Goal[]> Query(string userId)
        {
            return await goalsGetDbOperations.GetByUser(userId);
        }
    }
}