using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using PopugJira.EventBus.Events.BusinessEvents;
using PopugJira.Notifications.Application.Commands;
using Serviced;

namespace PopugJira.Notifications.Consumers
{
    public class PayEarnedToEmployeesEventConsumer : IConsumeAsync<PayEarnedToEmployeesEventV1>, IScoped
    {
        private readonly NotifyAboutEarnedCommand notifyAboutEarnedCommand;

        public PayEarnedToEmployeesEventConsumer(NotifyAboutEarnedCommand notifyAboutEarnedCommand)
        {
            this.notifyAboutEarnedCommand = notifyAboutEarnedCommand;
        }
        
        public async Task ConsumeAsync(PayEarnedToEmployeesEventV1 message, CancellationToken cancellationToken = new CancellationToken())
        {
            await notifyAboutEarnedCommand.Execute(message.AccountId, message.Earned);
        }
    }
}