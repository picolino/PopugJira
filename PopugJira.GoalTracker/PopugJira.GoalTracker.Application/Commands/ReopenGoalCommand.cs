using System.Threading.Tasks;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain.Definitions;
using Serviced;

namespace PopugJira.GoalTracker.Application.Commands
{
    public class ReopenGoalCommand : IScoped
    {
        private readonly IGoalsWriteDbOperations goalsWriteDbOperations;

        public ReopenGoalCommand(IGoalsWriteDbOperations goalsWriteDbOperations)
        {
            this.goalsWriteDbOperations = goalsWriteDbOperations;
        }
        
        public async Task Execute(string id)
        {
            await goalsWriteDbOperations.SetState(GoalState.Incomplete, id);
        }
    }
}