using System.Threading.Tasks;
using PopugJira.Analytics.DataAccessLayer.Contract;
using Serviced;

namespace PopugJira.Analytics.Application.Commands
{
    public class UpdateGoalCostCommand : IScoped
    {
        private readonly IGoalCostWriteDbOperations goalCostWriteDbOperations;

        public UpdateGoalCostCommand(IGoalCostWriteDbOperations goalCostWriteDbOperations)
        {
            this.goalCostWriteDbOperations = goalCostWriteDbOperations;
        }
        
        public async Task Execute(string goalId, string title)
        {
            await goalCostWriteDbOperations.Update(goalId, title);
        }
    }
}