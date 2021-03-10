using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain;

namespace PopugJira.GoalTracker.Application.Queries
{
    public class GoalQuery : IQuery
    {
        private readonly IGoalsDataContext goalsDataContext;

        public GoalQuery(IGoalsDataContext goalsDataContext)
        {
            this.goalsDataContext = goalsDataContext;
        }
        
        public async Task<Goal> Query(int id)
        {
            return await goalsDataContext.Get(id);
        }
    }
}