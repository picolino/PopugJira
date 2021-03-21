using FluentMigrator;

namespace PopugJira.Analytics.DataAccessLayer.Migrations
{
    [Migration(1)]
    public class AddGoalCosts : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("goal_costs")
                  .WithColumn("id").AsString().PrimaryKey()
                  .WithColumn("title").AsString().NotNullable()
                  .WithColumn("cost").AsDecimal().NotNullable()
                  .WithColumn("complete_datetime").AsDateTime2().Nullable();
        }
    }
}