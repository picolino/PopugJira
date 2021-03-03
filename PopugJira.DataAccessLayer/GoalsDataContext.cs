using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.DataAccessLayer.Contract;
using PopugJira.DataAccessLayer.Entities;
using PopugJira.Domain;

namespace PopugJira.DataAccessLayer
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
                                                     GoalStateId = goal.State.Id
                                                 });
        }
        
        public async Task<Goal> Get(int id)
        {
            var entity = await Goals.LoadWith(o => o.GoalState)
                                    .SingleOrDefaultAsync(o => o.Id == id);
            return entity.ToDomain();
        }

        public async Task Delete(int id)
        {
            await Goals.DeleteAsync(o => o.Id == id);
        }

        public async Task SetState(int goalId, int goalStateId)
        {
            await Goals.Where(o => o.Id == goalId)
                       .Set(o => o.GoalStateId, goalStateId)
                       .UpdateAsync();
        }
    }
}