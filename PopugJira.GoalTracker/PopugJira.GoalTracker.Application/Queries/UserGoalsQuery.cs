using System;
using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain;

namespace PopugJira.GoalTracker.Application.Queries
{
    public class UserGoalsQuery : IQuery
    {
        private readonly IGoalsDataContext goalsDataContext;

        public UserGoalsQuery(IGoalsDataContext goalsDataContext)
        {
            this.goalsDataContext = goalsDataContext;
        }

        public async Task<Goal[]> Query(string userId)
        {
            return await goalsDataContext.GetByUser(userId);
        }
    }
}