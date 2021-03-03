using FluentMigrator;

namespace PopugJira.DataAccessLayer.Migrations
{
    [Migration(0)]
    public class AddGoals : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("goal_states")
                  .WithColumn("id").AsInt32().PrimaryKey().Identity()
                  .WithColumn("name").AsString().ReferencedBy("goals", "goal_state_id");

            Create.Table("goals")
                  .WithColumn("id").AsInt32().PrimaryKey().Identity()
                  .WithColumn("description").AsString().Nullable()
                  .WithColumn("goal_state_id").AsInt32().ForeignKey("goal_states", "id").NotNullable().Indexed();

            Insert.IntoTable("goal_states")
                  .Row(new {name = "Open"})
                  .Row(new {name = "Closed"});
        }
    }
}