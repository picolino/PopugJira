using System.Threading.Tasks;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain;
using Serviced;

namespace PopugJira.GoalTracker.Application.Queries
{
    public class UserGoalsQuery : IScoped
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