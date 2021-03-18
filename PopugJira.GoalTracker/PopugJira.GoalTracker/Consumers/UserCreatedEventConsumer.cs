using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using PopugJira.EventBus.Events.UserCud;
using PopugJira.GoalTracker.Application.Commands;
using PopugJira.GoalTracker.Application.Dto;
using Serviced;

namespace PopugJira.GoalTracker.Consumers
{
    public class UserCreatedEventConsumer : IConsumeAsync<UserCreatedEvent>, IScoped
    {
        private readonly CreateAssigneeCommand createAssigneeCommand;

        public UserCreatedEventConsumer(CreateAssigneeCommand createAssigneeCommand)
        {
            this.createAssigneeCommand = createAssigneeCommand;
        }
        
        public async Task ConsumeAsync(UserCreatedEvent message, CancellationToken cancellationToken = new CancellationToken())
        {
            var assigneeCreateRequest = new AssigneeCreateDto
                                        {
                                            Id = message.Id,
                                            Name = message.Name
                                        };
            
            await createAssigneeCommand.Execute(assigneeCreateRequest);
        }
    }
}