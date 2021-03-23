using System;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.DataAccessLayer.Entities;
using PopugJira.GoalTracker.Domain;
using Serviced;

namespace PopugJira.GoalTracker.DataAccessLayer
{
    public class AssigneeWriteDbOperations : AssigneeDbOperations, IAssigneeWriteDbOperations, IScoped<IAssigneeWriteDbOperations>
    {
        public AssigneeWriteDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }

        public async Task Create(Assignee assignee)
        {
            await Assignees.InsertAsync(() => new AssigneeEntity
                                              {
                                                  Id = assignee.Id ?? Guid.NewGuid().ToString(),
                                                  Name = assignee.UserName
                                              });
        }
    }
}