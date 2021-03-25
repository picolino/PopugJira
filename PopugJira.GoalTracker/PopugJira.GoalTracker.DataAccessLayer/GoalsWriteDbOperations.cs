using System;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Tools;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.DataAccessLayer.Entities;
using PopugJira.GoalTracker.Domain;
using PopugJira.GoalTracker.Domain.Definitions;
using Serviced;

namespace PopugJira.GoalTracker.DataAccessLayer
{
    public class GoalsWriteDbOperations : GoalsDbOperations, IGoalsWriteDbOperations, IScoped<IGoalsWriteDbOperations>
    {
        public GoalsWriteDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }
        
        public async Task Create(Goal goal)
        {
            await Goals.InsertAsync(() => new GoalEntity
                                          {
                                              Id = goal.Id ?? Guid.NewGuid().ToString(),
                                              Title = goal.Title,
                                              Description = goal.Description,
                                              State = goal.State
                                          });
        }

        public async Task SetAssignee(string goalId, string assigneeId)
        {
            await Goals.Where(o => o.Id == goalId)
                       .Set(o => o.AssigneeId, assigneeId)
                       .UpdateAsync();
        }
        
        public async Task Update(string id, string title, string description)
        {
            await Goals.Where(o => o.Id == id)
                       .Set(o => o.Title, title)
                       .Set(o => o.Description, description)
                       .UpdateAsync();
        }

        public async Task Delete(string id)
        {
            await Goals.DeleteAsync(o => o.Id == id);
        }

        public async Task SetState(GoalState goalState, params string[] goalIds)
        {
            await Goals.Where(o => o.Id.In(goalIds))
                       .Set(o => o.State, goalState)
                       .UpdateAsync();
        }
    }
}