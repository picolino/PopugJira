using System.Threading.Tasks;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class ReopenGoalCommand
    {
        private readonly IGoalsDataContext goalsDataContext;

        public ReopenGoalCommand(IGoalsDataContext goalsDataContext)
        {
            this.goalsDataContext = goalsDataContext;
        }
        
        public async Task Execute(int id)
        {
            await goalsDataContext.SetState(id, GoalState.Incomplete);
        }
    }
}