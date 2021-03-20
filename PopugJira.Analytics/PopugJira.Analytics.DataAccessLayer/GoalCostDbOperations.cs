using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.Analytics.DataAccessLayer.Entities;

namespace PopugJira.Analytics.DataAccessLayer
{
    public class GoalCostDbOperations : SQLiteDatabaseConnection
    {
        protected ITable<GoalCostEntity> GoalCosts => GetTable<GoalCostEntity>();
        
        public GoalCostDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }
    }
}