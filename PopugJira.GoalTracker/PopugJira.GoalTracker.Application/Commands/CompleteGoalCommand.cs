using System;
using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class CompleteGoalCommand : ICommand
    {
        private readonly IGoalsWriteDbOperations goalsWriteDbOperations;

        public CompleteGoalCommand(IGoalsWriteDbOperations goalsWriteDbOperations)
        {
            this.goalsWriteDbOperations = goalsWriteDbOperations;
        }
        
        public async Task Execute(string id)
        {
            await goalsWriteDbOperations.SetState(GoalState.Complete, id);
        }
    }
}