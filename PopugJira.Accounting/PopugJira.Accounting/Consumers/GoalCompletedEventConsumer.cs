using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using PopugJira.Accounting.Application.Commands;
using PopugJira.Accounting.Application.Dtos;
using PopugJira.EventBus.Events.BusinessEvents;
using Serviced;

namespace PopugJira.Accounting.Consumers
{
    public class GoalCompletedEventConsumer : IConsumeAsync<GoalCompletedEventV1>, IScoped
    {
        private readonly CreateTransactionCommand createTransactionCommand;

        public GoalCompletedEventConsumer(CreateTransactionCommand createTransactionCommand)
        {
            this.createTransactionCommand = createTransactionCommand;
        }
        
        public async Task ConsumeAsync(GoalCompletedEventV1 message, CancellationToken cancellationToken = new CancellationToken())
        {
            await createTransactionCommand.Execute(new CreateTransactionDto
                                                   {
                                                       Credit = 0,
                                                       Debit = message.CompletePrice,
                                                       AccountId = message.AssigneeId,
                                                       DateTime = message.CompleteDateTime,
                                                       Reason = $"Goal completed ({message.Id})"
                                                   });
        }
    }
}