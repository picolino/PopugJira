using System;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.Analytics.DataAccessLayer.Contract;
using PopugJira.Analytics.DataAccessLayer.Entities;
using PopugJira.Analytics.Domain;
using Serviced;

namespace PopugJira.Analytics.DataAccessLayer
{
    public class GoalCostWriteDbOperations : GoalCostDbOperations, IGoalCostWriteDbOperations, IScoped<IGoalCostWriteDbOperations>
    {
        public GoalCostWriteDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }

        public async Task Create(GoalCost goalCost)
        {
            await GoalCosts.InsertAsync(() => new GoalCostEntity
                                              {
                                                  Id = goalCost.Id,
                                                  Title = goalCost.Title,
                                                  Cost = goalCost.Cost,
                                                  CompleteDateTime = goalCost.CompletedDateTime
                                              });
        }

        public async Task Complete(string goalId, DateTime completeDateTime)
        {
            await GoalCosts.Where(o => o.Id == goalId)
                           .Set(o => o.CompleteDateTime, completeDateTime)
                           .UpdateAsync();
        }

        public async Task Update(string goalId, string title)
        {
            await GoalCosts.Where(o => o.Id == goalId)
                           .Set(o => o.Title, title)
                           .UpdateAsync();
        }

        public async Task SetCost(string goalId, decimal cost)
        {
            await GoalCosts.Where(o => o.Id == goalId)
                           .Set(o => o.Cost, cost)
                           .UpdateAsync();
        }
    }
}