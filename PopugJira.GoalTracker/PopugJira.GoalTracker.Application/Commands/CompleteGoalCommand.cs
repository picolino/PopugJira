using System.Threading.Tasks;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class CompleteGoalCommand
    {
        private readonly IGoalsDataContext goalsDataContext;

        public CompleteGoalCommand(IGoalsDataContext goalsDataContext)
        {
            this.goalsDataContext = goalsDataContext;
        }
        
        public async Task Execute(int id)
        {
            await goalsDataContext.SetState(id, GoalState.Complete);
        }
    }
}