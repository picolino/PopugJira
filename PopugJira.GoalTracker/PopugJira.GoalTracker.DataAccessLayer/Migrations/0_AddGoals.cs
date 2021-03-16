using FluentMigrator;

namespace PopugJira.GoalTracker.DataAccessLayer.Migrations
{
    [Migration(0)]
    public class AddGoals : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("goals")
                  .WithColumn("id").AsString().PrimaryKey()
                  .WithColumn("title").AsString().NotNullable().Indexed()
                  .WithColumn("description").AsString().Nullable()
                  .WithColumn("state").AsInt32().NotNullable().Indexed();
        }
    }
}