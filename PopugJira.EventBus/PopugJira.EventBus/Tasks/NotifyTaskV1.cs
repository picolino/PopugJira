using System;

namespace PopugJira.EventBus.Tasks
{
    public class NotifyTaskV1
    {
        public string UserId { get; set; }
        public string Message { get; set; }
        public NotifyBy NotifyBy { get; set; }
    }

    [Flags]
    public enum NotifyBy
    {
        Email = 1,
        Sms = 2,
        Slack = 4
    }
}