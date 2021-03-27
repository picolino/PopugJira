using System;
using System.Threading.Tasks;
using PopugJira.Analytics.DataAccessLayer.Contract;
using Serviced;

namespace PopugJira.Analytics.Application.Commands
{
    public class CompleteGoalCostCommand : IScoped
    {
        private readonly IGoalCostWriteDbOperations goalCostWriteDbOperations;

        public CompleteGoalCostCommand(IGoalCostWriteDbOperations goalCostWriteDbOperations)
        {
            this.goalCostWriteDbOperations = goalCostWriteDbOperations;
        }
        
        public async Task Execute(string goalId, DateTime completeDateTime)
        {
            await goalCostWriteDbOperations.Complete(goalId, completeDateTime);
        }
    }
}