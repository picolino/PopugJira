using System.Threading.Tasks;
using PopugJira.Notifications.Application.Dtos;
using PopugJira.Notifications.DataAccessLayer.Contrcat;
using PopugJira.Notifications.Domain;
using Serviced;

namespace PopugJira.Notifications.Application.Commands
{
    public class CreateUserNotificationSettingsCommand : IScoped
    {
        private readonly IUserNotificationSettingsWriteDbOperations userNotificationSettingsWriteDbOperations;

        public CreateUserNotificationSettingsCommand(IUserNotificationSettingsWriteDbOperations userNotificationSettingsWriteDbOperations)
        {
            this.userNotificationSettingsWriteDbOperations = userNotificationSettingsWriteDbOperations;
        }

        public async Task Execute(CreateUserNotificationSettingsDto createUserNotificationSettingsDto)
        {
            string phone = null;
            var email = $"{createUserNotificationSettingsDto.UserName}@popugjira.dev";
            var slack = $"@{createUserNotificationSettingsDto.UserName}";
            var settings = new UserNotificationSettings(createUserNotificationSettingsDto.Id, phone, email, slack);
            await userNotificationSettingsWriteDbOperations.Create(settings);
        }
    }
}