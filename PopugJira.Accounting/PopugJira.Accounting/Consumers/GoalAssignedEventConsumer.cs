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
    public class GoalAssignedEventConsumer : IConsumeAsync<GoalAssignedEventV1>, IScoped
    {
        private readonly CreateTransactionCommand createTransactionCommand;
        private readonly IEstimatedGoalsGetDbOperations estimatedGoalsGetDbOperations;

        public GoalAssignedEventConsumer(CreateTransactionCommand createTransactionCommand,
                                         IEstimatedGoalsGetDbOperations estimatedGoalsGetDbOperations)
        {
            this.createTransactionCommand = createTransactionCommand;
            this.estimatedGoalsGetDbOperations = estimatedGoalsGetDbOperations;
        }
        
        public async Task ConsumeAsync(GoalAssignedEventV1 message, CancellationToken cancellationToken = new CancellationToken())
        {
            var estimatedGoal = await estimatedGoalsGetDbOperations.Get(message.Id);
            await createTransactionCommand.Execute(new CreateTransactionDto
                                                   {
                                                       Credit = estimatedGoal.AssignPrice,
                                                       Debit = 0,
                                                       AccountId = message.AssigneeId,
                                                       DateTime = message.AssignDateTime,
                                                       Reason = $"Goal assigned ({message.Id})"
                                                   });
        }
    }
}