using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.GoalTracker.Application.Dto;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class CreateGoalCommand : ICommand
    {
        private readonly IGoalsWriteDbOperations goalsWriteDbOperations;

        public CreateGoalCommand(IGoalsWriteDbOperations goalsWriteDbOperations)
        {
            this.goalsWriteDbOperations = goalsWriteDbOperations;
        }

        public async Task Execute(GoalCreateDto goalCreateDto)
        {
            var goal = new Goal(null, goalCreateDto.Title, goalCreateDto.Description, GoalState.Incomplete);
            await goalsWriteDbOperations.Create(goal);
        }
    }
}