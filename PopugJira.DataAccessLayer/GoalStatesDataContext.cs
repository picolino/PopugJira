using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.DataAccessLayer.Contract;
using PopugJira.Domain;
using PopugJira.Domain.Definitions;

namespace PopugJira.DataAccessLayer
{
    public class GoalStatesDataContext : SQLiteDatabaseConnection, IGoalStatesDataContext
    {
        public GoalStatesDataContext(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }

        public async Task<GoalState> GetOpenState()
        {
            var entity = await GoalStates.SingleAsync(o => o.Name == nameof(SystemGoalState.Open) 
                                                           && o.IsSystem);
            return entity.ToDomain();
        }

        public async Task<GoalState> GetClosedState()
        {
            var entity = await GoalStates.SingleAsync(o => o.Name == nameof(SystemGoalState.Closed) 
                                                           && o.IsSystem);
            return entity.ToDomain();
        }
    }
}