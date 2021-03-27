using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.Accounting.DataAccessLayer.Entities;

namespace PopugJira.Accounting.DataAccessLayer
{
    public class EstimatedGoalsDbOperations : SQLiteDatabaseConnection
    {
        protected ITable<EstimatedGoalEntity> EstimatedGoals => GetTable<EstimatedGoalEntity>();
        
        public EstimatedGoalsDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }
    }
}