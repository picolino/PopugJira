using System;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.GoalTracker.DataAccessLayer.Contract;
using PopugJira.GoalTracker.DataAccessLayer.Entities;
using Serviced;

namespace PopugJira.GoalTracker.DataAccessLayer
{
    public class AssigneesGetDbOperations : SQLiteDatabaseConnection, IAssigneesGetDbOperations, IScoped<IAssigneesGetDbOperations>
    {
        private ITable<AssigneeEntity> Assignees => GetTable<AssigneeEntity>();
        
        public AssigneesGetDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }

        public async Task<Guid[]> GetAllIds()
        {
            return await Assignees.Select(o => o.Id)
                                  .ToArrayAsync();
        }
    }
}