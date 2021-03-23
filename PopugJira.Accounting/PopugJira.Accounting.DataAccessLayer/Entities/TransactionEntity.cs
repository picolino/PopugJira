using System;
using LinqToDB.Mapping;
using PopugJira.Accounting.Domain;

namespace PopugJira.Accounting.DataAccessLayer.Entities
{
    [Table("transactions")]
    public class TransactionEntity
    {
        [Column("id"), PrimaryKey]
        public string Id { get; init; }
        
        [Column("account_id")]
        public string AccountId { get; init; }
        
        [Column("datetime")]
        public DateTime DateTime { get; init; }
        
        [Column("debit")]
        public decimal Debit { get; init; }
        
        [Column("credit")]
        public decimal Credit { get; init; }
        
        [Column("reason")]
        public string Reason { get; init; }
        
        [Association(ThisKey = nameof(AccountId), OtherKey = nameof(AccountEntity.Id))]
        public AccountEntity Account { get; init; }

        public Transaction ToDomain()
        {
            return new Transaction(Id, Account.ToDomain(), DateTime, Debit, Credit, Reason);
        }
    }
}