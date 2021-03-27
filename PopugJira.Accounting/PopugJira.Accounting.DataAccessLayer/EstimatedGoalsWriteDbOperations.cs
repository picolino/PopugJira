using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Configuration;
using PopugJira.Accounting.DataAccessLayer.Contract;
using PopugJira.Accounting.DataAccessLayer.Entities;
using PopugJira.Accounting.Domain;
using Serviced;

namespace PopugJira.Accounting.DataAccessLayer
{
    public class EstimatedGoalsWriteDbOperations : EstimatedGoalsDbOperations, IEstimatedGoalsWriteDbOperations, IScoped<IEstimatedGoalsWriteDbOperations>
    {
        public EstimatedGoalsWriteDbOperations(LinqToDbConnectionOptions<SQLiteDatabaseConnection> options) : base(options)
        {
        }

        public async Task Create(EstimatedGoal estimatedGoal)
        {
            await EstimatedGoals.InsertAsync(() => new EstimatedGoalEntity
                                                   {
                                                       Id = estimatedGoal.Id,
                                                       AssignPrice = estimatedGoal.AssignPrice,
                                                       CompletePrice = estimatedGoal.CompletePrice
                                                   });
        }
    }
}