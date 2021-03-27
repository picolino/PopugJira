using LinqToDB.Mapping;
using PopugJira.Notifications.Domain;

namespace PopugJira.Notifications.DataAccessLayer.Entities
{
    [Table("user_notification_settings")]
    public class UserNotificationSettingEntity
    {
        [Column("id"), PrimaryKey]
        public string Id { get; set; }
        
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        
        [Column("email")]
        public string Email { get; set; }
        
        [Column("slack_nickname")]
        public string SlackNickname { get; set; }

        public UserNotificationSettings ToDomain()
        {
            return new UserNotificationSettings(Id, PhoneNumber, Email, SlackNickname);
        }
    }
}