using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain;

namespace PopugJira.GoalTracker.Application.Queries
{
    public class AllGoalsQuery : IQuery
    {
        private readonly IGoalsDataContext goalsDataContext;

        public AllGoalsQuery(IGoalsDataContext goalsDataContext)
        {
            this.goalsDataContext = goalsDataContext;
        }
        
        public async Task<Goal[]> Query()
        {
            return await goalsDataContext.GetAll();
        }
    }
}