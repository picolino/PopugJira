using System;
using System.Threading.Tasks;
using PopugJira.Analytics.DataAccessLayer.Contract;
using PopugJira.Analytics.Domain;
using Serviced;

namespace PopugJira.Analytics.Application.Queries
{
    public class TopManagementEarnedForDayQuery : IScoped
    {
        private readonly ITopManagementEarnedEntryGetDbOperations topManagementEarnedEntryGetDbOperations;

        public TopManagementEarnedForDayQuery(ITopManagementEarnedEntryGetDbOperations topManagementEarnedEntryGetDbOperations)
        {
            this.topManagementEarnedEntryGetDbOperations = topManagementEarnedEntryGetDbOperations;
        }
        
        public async Task<TopManagementEarnedEntry[]> Query(DateTime date)
        {
            return await topManagementEarnedEntryGetDbOperations.GetByDate(date);
        }
    }
}