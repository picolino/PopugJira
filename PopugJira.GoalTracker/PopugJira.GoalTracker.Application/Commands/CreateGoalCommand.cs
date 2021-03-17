using System.Threading.Tasks;
using PopugJira.GoalTracker.Application.Dto;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain;
using PopugJira.GoalTracker.Domain.Definitions;
using Serviced;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class CreateGoalCommand : IScoped
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