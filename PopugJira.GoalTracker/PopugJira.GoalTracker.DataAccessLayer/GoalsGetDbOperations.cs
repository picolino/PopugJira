using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.Domain;
using PopugJira.GoalTracker.Domain.Definitions;
using Serviced;

namespace PopugJira.GoalTracker.DataAccessLayer
{
    public class GoalsGetDbOperations : GoalsDbOperations, IGoalsGetDbOperations, IScoped<IGoalsGetDbOperations>
    {
        public GoalsGetDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }

        public async Task<Goal[]> GetAll()
        {
            var entities = await Goals.LoadWith(o => o.Assignee)
                                      .ToArrayAsync();
            return entities.Select(o => o.ToDomain()).ToArray();
        }

        public async Task<Goal[]> GetByUser(string userId)
        {
            var entities = await Goals.LoadWith(o => o.Assignee)
                                      .Where(o => o.AssigneeId == userId)
                                      .ToArrayAsync();
            return entities.Select(o => o.ToDomain()).ToArray();
        }

        public async Task<string[]> GetIdsByState(GoalState state)
        {
            return await Goals.Where(o => o.State == state)
                              .Select(o => o.Id)
                              .ToArrayAsync();
        }

        public async Task<Goal> Get(string id)
        {
            var entity = await Goals.LoadWith(o => o.Assignee)
                                    .SingleOrDefaultAsync(o => o.Id == id);
            return entity?.ToDomain();
        }
    }
}