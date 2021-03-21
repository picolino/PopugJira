using FluentMigrator;

namespace PopugJira.Notifications.DataAccessLayer.Migrations
{
    [Migration(0)]
    public class AddUserNotificationSettings : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("user_notification_settings")
                  .WithColumn("id").AsString().PrimaryKey()
                  .WithColumn("phone_number").AsString().Nullable()
                  .WithColumn("email").AsString().Nullable()
                  .WithColumn("slack_nickname").AsString().Nullable();
        }
    }
}