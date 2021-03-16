namespace PopugJira.EventBus.Events.UserCud
{
    public class UserCreatedEvent
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}