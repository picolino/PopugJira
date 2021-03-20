using System;
using System.Threading.Tasks;

namespace PopugJira.Analytics.DataAccessLayer.Contract
{
    public interface ITopManagementEarnedEntryWriteDbOperations
    {
        Task AddEarnedWithNegativeEmployeesCountIncrementOrCreateNew(DateTime date, decimal addEarnedAmount);
    }
}