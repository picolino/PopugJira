using System;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.DataAccessLayer.Entities;
using PopugJira.GoalTracker.Domain;
using PopugJira.GoalTracker.Domain.Definitions;
using Serviced;

namespace PopugJira.GoalTracker.DataAccessLayer
{
    public class GoalsDataContext : SQLiteDatabaseConnection, IGoalsDataContext, IScoped<IGoalsDataContext>
    {
        public GoalsDataContext(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }

        public async Task Create(Goal goal)
        {
            await Goals.InsertAsync(() => new GoalEntity
                                          {
                                              Id = goal.Id ?? Guid.NewGuid(),
                                              Title = goal.Title,
                                              Description = goal.Description,
                                              State = goal.State
                                          });
        }

        public async Task<Goal[]> GetAll()
        {
            var entities = await Goals.ToArrayAsync();
            return entities.Select(o => o.ToDomain()).ToArray();
        }
        
        public async Task<Goal> Get(Guid id)
        {
            var entity = await Goals.SingleOrDefaultAsync(o => o.Id == id);
            return entity?.ToDomain();
        }

        public async Task Update(Guid id, string title, string description)
        {
            await Goals.Where(o => o.Id == id)
                       .Set(o => o.Title, title)
                       .Set(o => o.Description, description)
                       .UpdateAsync();
        }

        public async Task Delete(Guid id)
        {
            await Goals.DeleteAsync(o => o.Id == id);
        }

        public async Task SetState(Guid goalId, GoalState goalState)
        {
            await Goals.Where(o => o.Id == goalId)
                       .Set(o => o.State, goalState)
                       .UpdateAsync();
        }
    }
}