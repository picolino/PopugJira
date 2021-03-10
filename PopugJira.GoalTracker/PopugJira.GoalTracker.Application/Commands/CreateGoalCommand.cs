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
        private readonly IGoalsDataContext goalsDataContext;

        public CreateGoalCommand(IGoalsDataContext goalsDataContext)
        {
            this.goalsDataContext = goalsDataContext;
        }

        public async Task Execute(GoalCreateDto goalCreateDto)
        {
            var goal = new Goal(null, goalCreateDto.Title, goalCreateDto.Description, GoalState.Incomplete);
            await goalsDataContext.Create(goal);
        }
    }
}