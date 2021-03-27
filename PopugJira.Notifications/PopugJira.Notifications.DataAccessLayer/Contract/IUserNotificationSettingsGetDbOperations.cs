using System.Threading.Tasks;
using PopugJira.Notifications.Domain;

namespace PopugJira.Notifications.DataAccessLayer.Contract
{
    public interface IUserNotificationSettingsGetDbOperations
    {
        Task<UserNotificationSettings> Get(string userId);
    }
}