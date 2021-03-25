using System.Threading.Tasks;
using PopugJira.Analytics.DataAccessLayer.Contract;
using Serviced;

namespace PopugJira.Analytics.Application.Commands
{
    public class SetCostForGoalCommand : IScoped
    {
        private readonly IGoalCostWriteDbOperations goalCostWriteDbOperations;

        public SetCostForGoalCommand(IGoalCostWriteDbOperations goalCostWriteDbOperations)
        {
            this.goalCostWriteDbOperations = goalCostWriteDbOperations;
        }
        
        public async Task Execute(string goalId, decimal cost)
        {
            await goalCostWriteDbOperations.SetCost(goalId, cost);
        }
    }
}