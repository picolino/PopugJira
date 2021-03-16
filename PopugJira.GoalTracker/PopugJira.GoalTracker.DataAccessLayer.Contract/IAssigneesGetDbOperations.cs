using System;
using System.Threading.Tasks;
using PopugJira.GoalTracker.Domain;

namespace PopugJira.GoalTracker.DataAccessLayer.Contract
{
    public interface IAssigneesGetDbOperations
    {
        Task<string[]> GetAllIds();
    }
}