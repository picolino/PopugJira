using System.Threading.Tasks;
using PopugJira.GoalTracker.DataAccessLayer.Contract;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class CloseGoalCommand
    {
        private readonly IGoalsDataContext goalsDataContext;
        private readonly IGoalStatesDataContext goalStatesDataContext;

        public CloseGoalCommand(IGoalsDataContext goalsDataContext,
                               IGoalStatesDataContext goalStatesDataContext)
        {
            this.goalsDataContext = goalsDataContext;
            this.goalStatesDataContext = goalStatesDataContext;
        }
        
        public async Task Execute(int id)
        {
            var closedState = await goalStatesDataContext.GetClosedState();
            await goalsDataContext.SetState(id, closedState.Id);
        }
    }
}