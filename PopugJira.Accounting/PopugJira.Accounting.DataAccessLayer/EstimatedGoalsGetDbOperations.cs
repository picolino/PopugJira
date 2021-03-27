using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.Accounting.DataAccessLayer.Contract;
using PopugJira.Accounting.Domain;
using Serviced;

namespace PopugJira.Accounting.DataAccessLayer
{
    public class EstimatedGoalsGetDbOperations : EstimatedGoalsDbOperations, IEstimatedGoalsGetDbOperations, IScoped<IEstimatedGoalsGetDbOperations>
    {
        public EstimatedGoalsGetDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }

        public async Task<EstimatedGoal> Get(string id)
        {
            var entity = await EstimatedGoals.Where(o => o.Id == id).FirstOrDefaultAsync();
            return entity.ToDomain();
        }
    }
}