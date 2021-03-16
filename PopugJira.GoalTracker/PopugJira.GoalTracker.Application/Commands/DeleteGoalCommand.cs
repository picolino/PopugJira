using System;
using System.Threading.Tasks;
using PopugJira.AutoDI;
using PopugJira.GoalTracker.DataAccessLayer.Contract;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class DeleteGoalCommand : ICommand
    {
        private readonly IGoalsDataContext goalsDataContext;

        public DeleteGoalCommand(IGoalsDataContext goalsDataContext)
        {
            this.goalsDataContext = goalsDataContext;
        }

        public async Task Execute(string id)
        {
            await goalsDataContext.Delete(id);
        }
    }
}