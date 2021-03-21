using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using PopugJira.EventBus.Events.BusinessEvents;
using PopugJira.Notifications.Application.Commands;
using Serviced;

namespace PopugJira.Notifications.Consumers
{
    public class GoalAssignedEventConsumer : IConsumeAsync<GoalAssignedEventV1>, IScoped
    {
        private readonly NotifyAboutAssignCommand notifyAboutAssignCommand;

        public GoalAssignedEventConsumer(NotifyAboutAssignCommand notifyAboutAssignCommand)
        {
            this.notifyAboutAssignCommand = notifyAboutAssignCommand;
        }
        
        public async Task ConsumeAsync(GoalAssignedEventV1 message, CancellationToken cancellationToken = new CancellationToken())
        {
            await notifyAboutAssignCommand.Execute(message.AssigneeId, message.Id);
        }
    }
}