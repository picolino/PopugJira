using System;

namespace PopugJira.Notifications.Domain
{
    public class UserNotificationSettings
    {
        public UserNotificationSettings(string id, string phoneNumber, string email, string slackNickname)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            Email = email;
            SlackNickname = slackNickname;
        }

        public string Id { get; }
        public string PhoneNumber { get; }
        public string Email { get; }
        public string SlackNickname { get; }
    }
}