using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using PopugJira.Analytics.Application.Commands;
using PopugJira.EventBus.Events.BusinessEvents;
using Serviced;

namespace PopugJira.Analytics.Consumers
{
    public class GoalCompletedEventConsumer : IConsumeAsync<GoalCompletedEventV1>, IScoped
    {
        private readonly CompleteGoalCostCommand completeGoalCostCommand;

        public GoalCompletedEventConsumer(CompleteGoalCostCommand completeGoalCostCommand)
        {
            this.completeGoalCostCommand = completeGoalCostCommand;
        }
        
        public async Task ConsumeAsync(GoalCompletedEventV1 message, CancellationToken cancellationToken = new CancellationToken())
        {
            await completeGoalCostCommand.Execute(message.Id, message.CompleteDateTime);
        }
    }
}