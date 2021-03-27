using System.Threading.Tasks;
using PopugJira.Analytics.Application.Dtos;
using PopugJira.Analytics.DataAccessLayer.Contract;
using PopugJira.Analytics.Domain;
using Serviced;

namespace PopugJira.Analytics.Application.Commands
{
    public class CreateGoalCostCommand : IScoped
    {
        private readonly IGoalCostWriteDbOperations goalCostWriteDbOperations;

        public CreateGoalCostCommand(IGoalCostWriteDbOperations goalCostWriteDbOperations)
        {
            this.goalCostWriteDbOperations = goalCostWriteDbOperations;
        }
        
        public async Task Execute(CreateGoalCostDto createGoalCostDto)
        {
            var newGoalCost = new GoalCost(createGoalCostDto.Id, createGoalCostDto.Title, null, null);
            await goalCostWriteDbOperations.Create(newGoalCost);
        }
    }
}