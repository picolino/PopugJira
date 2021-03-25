using FluentMigrator;

namespace PopugJira.Accounting.DataAccessLayer.Migrations
{
    [Migration(2)]
    public class AddEstimatedGoals : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("estimated_goals")
                  .WithColumn("id").AsString().PrimaryKey()
                  .WithColumn("assign_price").AsDecimal().NotNullable()
                  .WithColumn("complete_price").AsDecimal().NotNullable();
        }
    }
}