using System;
using System.Threading.Tasks;
using PopugJira.Notifications.DataAccessLayer.Contract;
using Serviced;

namespace PopugJira.Notifications.Application.Commands
{
    public class NotifyByPhoneSmsCommand : IScoped
    {
        private readonly IUserNotificationSettingsGetDbOperations userNotificationSettingsGetDbOperations;

        public NotifyByPhoneSmsCommand(IUserNotificationSettingsGetDbOperations userNotificationSettingsGetDbOperations)
        {
            this.userNotificationSettingsGetDbOperations = userNotificationSettingsGetDbOperations;
        }
        
        public async Task Execute(string userId, string message)
        {
            var settings = await userNotificationSettingsGetDbOperations.Get(userId);
            Console.WriteLine($"Phone sms notification to '{settings.PhoneNumber}' with message '{message}'");
        }
    }
}