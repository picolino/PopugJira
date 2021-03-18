using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using PopugJira.Accounting.Application.Commands;
using PopugJira.Accounting.Application.Dtos;
using PopugJira.EventBus.Events.BusinessEvents;
using Serviced;

namespace PopugJira.Accounting.Consumers
{
    public class GoalAssignedEventConsumer : IConsumeAsync<GoalAssignedEvent>, IScoped
    {
        private readonly CreateTransactionCommand createTransactionCommand;

        public GoalAssignedEventConsumer(CreateTransactionCommand createTransactionCommand)
        {
            this.createTransactionCommand = createTransactionCommand;
        }
        
        public async Task ConsumeAsync(GoalAssignedEvent message, CancellationToken cancellationToken = new CancellationToken())
        {
            await createTransactionCommand.Execute(new CreateTransactionDto
                                                   {
                                                       Credit = message.AssignPrice,
                                                       Debit = 0,
                                                       AccountId = message.AssigneeId,
                                                       DateTime = message.AssignDateTime,
                                                       Reason = $"Goal assigned ({message.Id})"
                                                   });
        }
    }
}