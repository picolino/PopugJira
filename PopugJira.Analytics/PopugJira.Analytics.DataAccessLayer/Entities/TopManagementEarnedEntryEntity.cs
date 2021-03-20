using System;
using LinqToDB.Mapping;
using PopugJira.Analytics.Domain;

namespace PopugJira.Analytics.DataAccessLayer.Entities
{
    [Table("top_management_earned_entries")]
    public record TopManagementEarnedEntryEntity
    {
        [Column("id"), PrimaryKey]
        public string Id { get; set; }
        
        [Column("date")]
        public DateTime Date { get; set; }
        
        [Column("earned")]
        public decimal Earned { get; set; }
        
        [Column("negative_balance_employees_count")]
        public int NegativeEmployeesBalanceCount { get; set; }

        public TopManagementEarnedEntry ToDomain()
        {
            return new TopManagementEarnedEntry(Id, Date, Earned, NegativeEmployeesBalanceCount);
        }
    }
}