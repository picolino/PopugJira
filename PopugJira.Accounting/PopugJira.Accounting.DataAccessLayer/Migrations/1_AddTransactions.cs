using FluentMigrator;

namespace PopugJira.Accounting.DataAccessLayer.Migrations
{
    [Migration(1)]
    public class AddTransactions : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("transactions")
                  .WithColumn("id").AsString().PrimaryKey()
                  .WithColumn("account_id").AsString().NotNullable().ForeignKey("accounts", "id")
                  .WithColumn("datetime").AsDateTime2().NotNullable()
                  .WithColumn("debit").AsDecimal().NotNullable()
                  .WithColumn("credit").AsDecimal().NotNullable()
                  .WithColumn("reason").AsString().NotNullable();
        }
    }
}