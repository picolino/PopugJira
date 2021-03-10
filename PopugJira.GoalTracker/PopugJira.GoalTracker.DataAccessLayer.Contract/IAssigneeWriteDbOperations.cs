using System.Threading.Tasks;
using PopugJira.GoalTracker.Domain;

namespace PopugJira.GoalTracker.DataAccessLayer.Contract
{
    public interface IAssigneeWriteDbOperations
    {
        Task Create(Assignee assignee);
    }
}