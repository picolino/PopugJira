using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using PopugJira.Accounting.Application.Commands;
using PopugJira.EventBus.Events.GoalCud;
using Serviced;

namespace PopugJira.Accounting.Consumers
{
    public class GoalCreatedEventConsumer : IConsumeAsync<GoalCreatedEventV1>, IScoped
    {
        private readonly EstimateGoalCommand estimateGoalCommand;

        public GoalCreatedEventConsumer(EstimateGoalCommand estimateGoalCommand)
        {
            this.estimateGoalCommand = estimateGoalCommand;
        }

        public async Task ConsumeAsync(GoalCreatedEventV1 message, CancellationToken cancellationToken = new CancellationToken())
        {
            await estimateGoalCommand.Execute(message.Id);
        }
    }
}