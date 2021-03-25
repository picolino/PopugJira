using System.Threading.Tasks;
using PopugJira.Accounting.Domain;

namespace PopugJira.Accounting.DataAccessLayer.Contract
{
    public interface IEstimatedGoalsWriteDbOperations
    {
        public Task Create(EstimatedGoal estimatedGoal);
    }
}