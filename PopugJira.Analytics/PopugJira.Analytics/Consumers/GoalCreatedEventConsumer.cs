using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using PopugJira.Analytics.Application.Commands;
using PopugJira.Analytics.Application.Dtos;
using PopugJira.EventBus.Events.GoalCud;
using Serviced;

namespace PopugJira.Analytics.Consumers
{
    public class GoalCreatedEventConsumer : IConsumeAsync<GoalCreatedEventV1>, IScoped
    {
        private readonly CreateGoalCostCommand createGoalCostCommand;

        public GoalCreatedEventConsumer(CreateGoalCostCommand createGoalCostCommand)
        {
            this.createGoalCostCommand = createGoalCostCommand;
        }
        
        public async Task ConsumeAsync(GoalCreatedEventV1 message, CancellationToken cancellationToken = new CancellationToken())
        {
            await createGoalCostCommand.Execute(new CreateGoalCostDto
                                                {
                                                    Id = message.Id,
                                                    Title = message.Title
                                                });
        }
    }
}