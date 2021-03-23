using FluentMigrator;

namespace PopugJira.GoalTracker.DataAccessLayer.Migrations
{
    [Migration(1)]
    public class AddAssignees : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("assignees")
                  .WithColumn("id").AsString().PrimaryKey()
                  .WithColumn("name").AsString().NotNullable();
            
            Alter.Table("goals")
                 .AddColumn("assignee_id").AsString().Nullable().ForeignKey("assignees", "id");
        }
    }
}