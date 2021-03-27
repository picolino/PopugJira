using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.Notifications.DataAccessLayer.Entities;

namespace PopugJira.Notifications.DataAccessLayer
{
    public class UserNotificationSettingsDbOperations : SQLiteDatabaseConnection
    {
        protected ITable<UserNotificationSettingEntity> UserNotificationSettings => GetTable<UserNotificationSettingEntity>();
        
        public UserNotificationSettingsDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }
    }
}