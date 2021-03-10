using FluentMigrator;

namespace PopugJira.GoalTracker.DataAccessLayer.Migrations
{
    [Migration(1)]
    public class AddAssignees : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("assignees")
                  .WithColumn("id").AsGuid().PrimaryKey()
                  .WithColumn("user_id").AsGuid().NotNullable()
                  .WithColumn("user_name").AsString().NotNullable();
            
            Alter.Table("goals")
                 .AddColumn("assignee_id").AsGuid().Nullable().ForeignKey("assignees", "id");
        }
    }
}