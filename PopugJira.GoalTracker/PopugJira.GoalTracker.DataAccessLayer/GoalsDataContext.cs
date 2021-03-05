using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Async;
using LinqToDB.Configuration;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.DataAccessLayer.Entities;
using PopugJira.GoalTracker.Domain;
using PopugJira.GoalTracker.Domain.Definitions;

namespace PopugJira.GoalTracker.DataAccessLayer
{
    public class GoalsDataContext : SQLiteDatabaseConnection, IGoalsDataContext
    {
        public GoalsDataContext(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }

        public async Task<int> Create(Goal goal)
        {
            return await Goals.InsertAsync(() => new GoalEntity
                                                 {
                                                     Description = goal.Description,
                                                     State = goal.State
                                                 });
        }

        public async Task<Goal[]> GetAll()
        {
            var entities = await Goals.ToArrayAsync();
            return entities.Select(o => o.ToDomain()).ToArray();
        }
        
        public async Task<Goal> Get(int id)
        {
            var entity = await Goals.SingleOrDefaultAsync(o => o.Id == id);
            return entity?.ToDomain();
        }

        public async Task Update(int id, string description)
        {
            await Goals.Where(o => o.Id == id)
                       .Set(o => o.Description, description)
                       .UpdateAsync();
        }

        public async Task Delete(int id)
        {
            await Goals.DeleteAsync(o => o.Id == id);
        }

        public async Task SetState(int goalId, GoalState goalState)
        {
            await Goals.Where(o => o.Id == goalId)
                       .Set(o => o.State, goalState)
                       .UpdateAsync();
        }
    }
}