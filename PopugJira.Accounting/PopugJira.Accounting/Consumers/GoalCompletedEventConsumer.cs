using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using PopugJira.EventBus.Events.BusinessEvents;

namespace PopugJira.Accounting.Consumers
{
    public class GoalCompletedEventConsumer : IConsumeAsync<GoalCompletedEvent>
    {
        public async Task ConsumeAsync(GoalCompletedEvent message, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }
    }
}