using System;
using System.Threading.Tasks;
using PopugJira.Notifications.DataAccessLayer.Contract;
using Serviced;

namespace PopugJira.Notifications.Application.Commands
{
    public class NotifyBySlackCommand : IScoped
    {
        private readonly IUserNotificationSettingsGetDbOperations userNotificationSettingsGetDbOperations;

        public NotifyBySlackCommand(IUserNotificationSettingsGetDbOperations userNotificationSettingsGetDbOperations)
        {
            this.userNotificationSettingsGetDbOperations = userNotificationSettingsGetDbOperations;
        }
        
        public async Task Execute(string userId, string message)
        {
            var settings = await userNotificationSettingsGetDbOperations.Get(userId);
            Console.WriteLine($"Slack notification to '{settings.SlackNickname}' with message '{message}'");
        }
    }
}