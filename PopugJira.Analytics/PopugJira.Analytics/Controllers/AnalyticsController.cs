using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PopugJira.Analytics.Application.Queries;
using PopugJira.Analytics.Domain;

namespace PopugJira.Analytics.Controllers
{
    [ApiController]
    [Route("api/v1/analytics")]
    public class AnalyticsController : ControllerBase
    {
        private readonly TopManagementEarnedForDayQuery topManagementEarnedForDayQuery;

        public AnalyticsController(TopManagementEarnedForDayQuery topManagementEarnedForDayQuery)
        {
            this.topManagementEarnedForDayQuery = topManagementEarnedForDayQuery;
        }
        
        [HttpGet("earned/management/{date}")]
        public async Task<TopManagementEarnedEntry[]> QueryTopManagementEarnedFor([FromQuery] DateTime date)
        {
            return await topManagementEarnedForDayQuery.Query(date);
        }
    }
}