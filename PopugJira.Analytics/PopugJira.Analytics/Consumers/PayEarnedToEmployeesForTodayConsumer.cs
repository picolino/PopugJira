using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using PopugJira.Analytics.Application.Commands;
using PopugJira.Analytics.Application.Dtos;
using PopugJira.EventBus.Events.BusinessEvents;
using Serviced;

namespace PopugJira.Analytics.Consumers
{
    public class PayEarnedToEmployeesForTodayConsumer : IConsumeAsync<PayEarnedToEmployeesEventV1>, IScoped
    {
        private readonly HandlePaymentCommand handlePaymentCommand;

        public PayEarnedToEmployeesForTodayConsumer(HandlePaymentCommand handlePaymentCommand)
        {
            this.handlePaymentCommand = handlePaymentCommand;
        }
        
        public async Task ConsumeAsync(PayEarnedToEmployeesEventV1 message, CancellationToken cancellationToken = new CancellationToken())
        {
            await handlePaymentCommand.Execute(new PaymentInfoDto
                                               {
                                                   Earned = message.Earned,
                                                   PaymentDateTime = message.PaymentDateTime
                                               });
        }
    }
}