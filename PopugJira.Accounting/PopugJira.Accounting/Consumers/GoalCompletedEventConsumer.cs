using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using PopugJira.Accounting.Application.Commands;
using PopugJira.Accounting.Application.Dtos;
using PopugJira.Accounting.DataAccessLayer.Contract;
using PopugJira.EventBus.Events.BusinessEvents;
using Serviced;

namespace PopugJira.Accounting.Consumers
{
    public class GoalCompletedEventConsumer : IConsumeAsync<GoalCompletedEventV1>, IScoped
    {
        private readonly CreateTransactionCommand createTransactionCommand;
        private readonly IEstimatedGoalsGetDbOperations estimatedGoalsGetDbOperations;

        public GoalCompletedEventConsumer(CreateTransactionCommand createTransactionCommand,
                                          IEstimatedGoalsGetDbOperations estimatedGoalsGetDbOperations)
        {
            this.createTransactionCommand = createTransactionCommand;
            this.estimatedGoalsGetDbOperations = estimatedGoalsGetDbOperations;
        }
        
        public async Task ConsumeAsync(GoalCompletedEventV1 message, CancellationToken cancellationToken = new CancellationToken())
        {
            var estimatedGoal = await estimatedGoalsGetDbOperations.Get(message.Id);
            await createTransactionCommand.Execute(new CreateTransactionDto
                                                   {
                                                       Credit = 0,
                                                       Debit = estimatedGoal.CompletePrice,
                                                       AccountId = message.AssigneeId,
                                                       DateTime = message.CompleteDateTime,
                                                       Reason = $"Goal completed ({message.Id})"
                                                   });
        }
    }
}