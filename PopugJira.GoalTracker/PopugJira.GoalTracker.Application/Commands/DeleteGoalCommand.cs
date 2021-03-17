using System.Threading.Tasks;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using Serviced;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class DeleteGoalCommand : IScoped
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