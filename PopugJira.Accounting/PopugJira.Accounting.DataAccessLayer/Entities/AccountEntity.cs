using LinqToDB.Mapping;
using PopugJira.Accounting.Domain;

namespace PopugJira.Accounting.DataAccessLayer.Entities
{
    [Table("accounts")]
    public record AccountEntity
    {
        [Column("id"), PrimaryKey]
        public string Id { get; init; }
        
        [Column("name")]
        public string Name { get; init; }
        
        [Column("balance")]
        public decimal Balance { get; init; }

        public Account ToDomain()
        {
            return new Account(Id, Name, Balance);
        }
    }
}