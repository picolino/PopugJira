using System.Threading.Tasks;
using PopugJira.Accounting.Domain;

namespace PopugJira.Accounting.DataAccessLayer.Contract
{
    public interface IEstimatedGoalsGetDbOperations
    {
        public Task<EstimatedGoal> Get(string id);
    }
}