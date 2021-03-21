using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.Notifications.DataAccessLayer.Contract;
using PopugJira.Notifications.Domain;
using Serviced;

namespace PopugJira.Notifications.DataAccessLayer
{
    public class UserNotificationSettingsGetDbOperations : UserNotificationSettingsDbOperations, IUserNotificationSettingsGetDbOperations, IScoped<IUserNotificationSettingsGetDbOperations>
    {
        public UserNotificationSettingsGetDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }

        public async Task<UserNotificationSettings> Get(string userId)
        {
            var entity = await UserNotificationSettings.Where(o => o.Id == userId)
                                                       .FirstOrDefaultAsync();
            return entity?.ToDomain();
        }
    }
}