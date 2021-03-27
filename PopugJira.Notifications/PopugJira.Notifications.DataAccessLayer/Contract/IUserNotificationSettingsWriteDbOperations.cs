using System.Threading.Tasks;
using PopugJira.Notifications.Domain;

namespace PopugJira.Notifications.DataAccessLayer.Contrcat
{
    public interface IUserNotificationSettingsWriteDbOperations
    {
        Task Create(UserNotificationSettings settings);
    }
}