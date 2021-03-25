using System.Threading.Tasks;
using PopugJira.Accounting.DataAccessLayer.Contract;
using PopugJira.Accounting.Domain;
using PopugJira.EventBus;
using PopugJira.EventBus.Events.GoalCud;
using Serviced;

namespace PopugJira.Accounting.Application.Commands
{
    public class EstimateGoalCommand : IScoped
    {
        private readonly IMessageBus messageBus;
        private readonly IGoalsConfigGetDbOperations goalsConfigGetDbOperations;
        private readonly IEstimatedGoalsWriteDbOperations estimatedGoalsWriteDbOperations;

        public EstimateGoalCommand(IMessageBus messageBus, 
                                   IGoalsConfigGetDbOperations goalsConfigGetDbOperations,
                                   IEstimatedGoalsWriteDbOperations estimatedGoalsWriteDbOperations)
        {
            this.messageBus = messageBus;
            this.goalsConfigGetDbOperations = goalsConfigGetDbOperations;
            this.estimatedGoalsWriteDbOperations = estimatedGoalsWriteDbOperations;
        }
        
        public async Task Execute(string goalId)
        {
            var assignPrice = await goalsConfigGetDbOperations.GetAssignGoalPrice();
            var completePrice = await goalsConfigGetDbOperations.GetCompleteGoalPrice();

            var estimatedGoal = new EstimatedGoal(goalId, assignPrice, completePrice);
            await estimatedGoalsWriteDbOperations.Create(estimatedGoal);
            
            await messageBus.Publish(new GoalUpdatedEventV1
                                     {
                                         Id = estimatedGoal.Id,
                                         EstimatePart = new GoalUpdatedEventV1PricePart
                                                     {
                                                         AssignPrice = estimatedGoal.AssignPrice,
                                                         CompletePrice = estimatedGoal.CompletePrice
                                                     }
                                     });
        }
    }
}