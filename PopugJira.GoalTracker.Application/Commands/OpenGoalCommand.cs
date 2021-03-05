using System.Threading.Tasks;
using PopugJira.GoalTracker.DataAccessLayer.Contract;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class OpenGoalCommand
    {
        private readonly IGoalsDataContext goalsDataContext;
        private readonly IGoalStatesDataContext goalStatesDataContext;

        public OpenGoalCommand(IGoalsDataContext goalsDataContext,
                               IGoalStatesDataContext goalStatesDataContext)
        {
            this.goalsDataContext = goalsDataContext;
            this.goalStatesDataContext = goalStatesDataContext;
        }
        
        public async Task Execute(int id)
        {
            var openState = await goalStatesDataContext.GetOpenState();
            await goalsDataContext.SetState(id, openState.Id);
        }
    }
}