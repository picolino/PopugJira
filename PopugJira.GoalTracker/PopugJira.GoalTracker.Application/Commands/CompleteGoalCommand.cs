using System;
using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class CompleteGoalCommand : ICommand
    {
        private readonly IGoalsDataContext goalsDataContext;

        public CompleteGoalCommand(IGoalsDataContext goalsDataContext)
        {
            this.goalsDataContext = goalsDataContext;
        }
        
        public async Task Execute(string id)
        {
            await goalsDataContext.SetState(GoalState.Complete, id);
        }
    }
}