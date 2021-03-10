using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.GoalTracker.Application.Dto;
using PopugJira.GoalTracker.DataAccessLayer.Contract;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class UpdateGoalCommand : ICommand
    {
        private readonly IGoalsDataContext goalsDataContext;

        public UpdateGoalCommand(IGoalsDataContext goalsDataContext)
        {
            this.goalsDataContext = goalsDataContext;
        }

        public async Task Execute(GoalUpdateDto goalUpdateDto)
        {
            await goalsDataContext.Update(goalUpdateDto.Id, goalUpdateDto.Title, goalUpdateDto.Description);
        }
    }
}