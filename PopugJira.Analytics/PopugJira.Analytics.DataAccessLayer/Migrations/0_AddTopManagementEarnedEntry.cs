using FluentMigrator;

namespace PopugJira.Analytics.DataAccessLayer.Migrations
{
    [Migration(0)]
    public class AddTopManagementEarnedEntry : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("top_management_earned_entries")
                  .WithColumn("id").AsString().PrimaryKey()
                  .WithColumn("date").AsDate().NotNullable().Unique()
                  .WithColumn("earned").AsDecimal().NotNullable()
                  .WithColumn("negative_balance_employees_count").AsInt32().NotNullable();
        }
    }
}