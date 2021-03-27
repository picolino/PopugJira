using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using PopugJira.EventBus.Events.UserCud;
using PopugJira.Notifications.Application.Commands;
using PopugJira.Notifications.Application.Dtos;
using Serviced;

namespace PopugJira.Notifications.Consumers
{
    public class UserCreatedEventConsumer : IConsumeAsync<UserCreatedEventV1>, IScoped
    {
        private readonly CreateUserNotificationSettingsCommand createUserNotificationSettingsCommand;

        public UserCreatedEventConsumer(CreateUserNotificationSettingsCommand createUserNotificationSettingsCommand)
        {
            this.createUserNotificationSettingsCommand = createUserNotificationSettingsCommand;
        }
        
        public async Task ConsumeAsync(UserCreatedEventV1 message, CancellationToken cancellationToken = new CancellationToken())
        {
            await createUserNotificationSettingsCommand.Execute(new CreateUserNotificationSettingsDto
                                                                {
                                                                    Id = message.Id,
                                                                    UserName = message.Name
                                                                });
        }
    }
}