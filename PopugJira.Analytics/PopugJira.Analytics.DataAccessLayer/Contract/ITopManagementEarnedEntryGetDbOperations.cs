using System;
using System.Threading.Tasks;
using PopugJira.Analytics.Domain;

namespace PopugJira.Analytics.DataAccessLayer.Contract
{
    public interface ITopManagementEarnedEntryGetDbOperations
    {
        Task<TopManagementEarnedEntry[]> GetByDate(DateTime date);
    }
}