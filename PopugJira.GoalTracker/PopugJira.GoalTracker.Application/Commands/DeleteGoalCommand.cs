using System;
using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.GoalTracker.DataAccessLayer.Contract;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class DeleteGoalCommand : ICommand
    {
        private readonly IGoalsWriteDbOperations goalsWriteDbOperations;

        public DeleteGoalCommand(IGoalsWriteDbOperations goalsWriteDbOperations)
        {
            this.goalsWriteDbOperations = goalsWriteDbOperations;
        }

        public async Task Execute(string id)
        {
            await goalsWriteDbOperations.Delete(id);
        }
    }
}