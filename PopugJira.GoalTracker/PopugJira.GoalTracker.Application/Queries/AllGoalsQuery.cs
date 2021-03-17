using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain;

namespace PopugJira.GoalTracker.Application.Queries
{
    public class AllGoalsQuery : IQuery
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