using FluentMigrator;

namespace PopugJira.Accounting.DataAccessLayer.Migrations
{
    [Migration(0)]
    public class AddAccounts : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("accounts")
                  .WithColumn("id").AsString().PrimaryKey()
                  .WithColumn("name").AsString().NotNullable()
                  .WithColumn("balance").AsDecimal().NotNullable();
        }
    }
}