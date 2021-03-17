using System.Threading.Tasks;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain;
using Serviced;

namespace PopugJira.GoalTracker.Application.Queries
{
    public class AllGoalsQuery : IScoped
    {
        private readonly IGoalsGetDbOperations goalsGetDbOperations;

        public AllGoalsQuery(IGoalsGetDbOperations goalsGetDbOperations)
        {
            this.goalsGetDbOperations = goalsGetDbOperations;
        }
        
        public async Task<Goal[]> Query()
        {
            return await goalsGetDbOperations.GetAll();
        }
    }
}