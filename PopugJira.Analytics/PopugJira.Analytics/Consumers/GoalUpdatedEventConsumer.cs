using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using PopugJira.Analytics.Application.Commands;
using PopugJira.EventBus.Events.GoalCud;
using Serviced;

namespace PopugJira.Analytics.Consumers
{
    public class GoalUpdatedEventConsumer : IConsumeAsync<GoalUpdatedEventV1>, IScoped
    {
        private readonly UpdateGoalCostCommand updateGoalCostCommand;
        private readonly SetCostForGoalCommand setCostForGoalCommand;

        public GoalUpdatedEventConsumer(UpdateGoalCostCommand updateGoalCostCommand,
                                        SetCostForGoalCommand setCostForGoalCommand)
        {
            this.updateGoalCostCommand = updateGoalCostCommand;
            this.setCostForGoalCommand = setCostForGoalCommand;
        }
        
        public async Task ConsumeAsync(GoalUpdatedEventV1 message, CancellationToken cancellationToken = new CancellationToken())
        {
            if (message.GoalPart is not null)
            {
                await updateGoalCostCommand.Execute(message.Id, message.GoalPart.Title);
            }
            
            if (message.EstimatePart is not null)
            {
                await setCostForGoalCommand.Execute(message.Id, message.EstimatePart.CompletePrice);
            }
        }
    }
}