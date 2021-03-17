using System.Threading.Tasks;
using PopugJira.GoalTracker.Application.Dto;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using Serviced;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class UpdateGoalCommand : IScoped
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