using System;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.Notifications.DataAccessLayer.Contrcat;
using PopugJira.Notifications.DataAccessLayer.Entities;
using PopugJira.Notifications.Domain;
using Serviced;

namespace PopugJira.Notifications.DataAccessLayer
{
    public class UserNotificationSettingsWriteDbOperations : UserNotificationSettingsDbOperations, IUserNotificationSettingsWriteDbOperations, IScoped<IUserNotificationSettingsWriteDbOperations>
    {
        public UserNotificationSettingsWriteDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }

        public async Task Create(UserNotificationSettings settings)
        {
            await UserNotificationSettings.InsertAsync(() => new UserNotificationSettingEntity
                                                             {
                                                                 Id = settings.Id ?? Guid.NewGuid().ToString(),
                                                                 PhoneNumber = settings.PhoneNumber,
                                                                 Email = settings.Email,
                                                                 SlackNickname = settings.SlackNickname
                                                             });
        }
    }
}