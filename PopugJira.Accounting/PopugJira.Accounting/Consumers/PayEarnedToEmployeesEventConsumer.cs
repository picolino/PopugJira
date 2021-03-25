using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using PopugJira.Accounting.Application.Commands;
using PopugJira.EventBus.Events.BusinessEvents;
using Serviced;

namespace PopugJira.Accounting.Consumers
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