using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.GoalTracker.Application.Dto;
using PopugJira.GoalTracker.DataAccessLayer.Contract;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class UpdateGoalCommand : ICommand
    {
        private readonly IGoalsWriteDbOperations goalsWriteDbOperations;

        public UpdateGoalCommand(IGoalsWriteDbOperations goalsWriteDbOperations)
        {
            this.goalsWriteDbOperations = goalsWriteDbOperations;
        }

        public async Task Execute(GoalUpdateDto goalUpdateDto)
        {
            await goalsWriteDbOperations.Update(goalUpdateDto.Id, goalUpdateDto.Title, goalUpdateDto.Description);
        }
    }
}