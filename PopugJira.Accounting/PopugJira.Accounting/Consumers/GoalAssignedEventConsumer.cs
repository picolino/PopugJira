using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using PopugJira.EventBus.Events.BusinessEvents;

namespace PopugJira.Accounting.Consumers
{
    public class GoalAssignedEventConsumer : IConsumeAsync<GoalAssignedEvent>
    {
        public async Task ConsumeAsync(GoalAssignedEvent message, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }
    }
}