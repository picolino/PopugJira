using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.DataAccessLayer
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