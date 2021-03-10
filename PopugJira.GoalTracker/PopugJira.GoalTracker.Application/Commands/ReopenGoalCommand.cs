using System;
using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class ReopenGoalCommand : ICommand
    {
        private readonly IGoalsDataContext goalsDataContext;

        public ReopenGoalCommand(IGoalsDataContext goalsDataContext)
        {
            this.goalsDataContext = goalsDataContext;
        }
        
        public async Task Execute(Guid id)
        {
            await goalsDataContext.SetState(GoalState.Incomplete, id);
        }
    }
}