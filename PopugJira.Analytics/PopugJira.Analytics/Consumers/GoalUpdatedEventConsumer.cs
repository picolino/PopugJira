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

        public GoalUpdatedEventConsumer(UpdateGoalCostCommand updateGoalCostCommand)
        {
            this.updateGoalCostCommand = updateGoalCostCommand;
        }
        
        public async Task ConsumeAsync(GoalUpdatedEventV1 message, CancellationToken cancellationToken = new CancellationToken())
        {
            if (message.GoalPart is not null)
            {
                
            }
            if (message.PricePart is not null)
            {
                await updateGoalCostCommand.Execute(message.Id, message.PricePart.Title);
            }
        }
    }
}