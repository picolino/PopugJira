using System;
using System.Threading.Tasks;
using PopugJira.Notifications.DataAccessLayer.Contract;
using Serviced;

namespace PopugJira.Notifications.Application.Commands
{
    public class NotifyByEmailCommand : IScoped
    {
        private readonly IUserNotificationSettingsGetDbOperations userNotificationSettingsGetDbOperations;

        public NotifyByEmailCommand(IUserNotificationSettingsGetDbOperations userNotificationSettingsGetDbOperations)
        {
            this.userNotificationSettingsGetDbOperations = userNotificationSettingsGetDbOperations;
        }
        
        public async Task Execute(string userId, string message)
        {
            var settings = await userNotificationSettingsGetDbOperations.Get(userId);
            Console.WriteLine($"Email notification to '{settings.Email}' with message '{message}'");
        }
    }
}