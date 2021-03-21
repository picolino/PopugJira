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
        private readonly MostCostlyGoalForPeriodQuery mostCostlyGoalForPeriodQuery;

        public AnalyticsController(TopManagementEarnedForDayQuery topManagementEarnedForDayQuery,
                                   MostCostlyGoalForPeriodQuery mostCostlyGoalForPeriodQuery)
        {
            this.topManagementEarnedForDayQuery = topManagementEarnedForDayQuery;
            this.mostCostlyGoalForPeriodQuery = mostCostlyGoalForPeriodQuery;
        }
        
        [HttpGet("earned/management")]
        public async Task<TopManagementEarnedEntry> QueryTopManagementEarnedFor([FromQuery] DateTime date)
        {
            return await topManagementEarnedForDayQuery.Query(date);
        }

        [HttpGet("goals/costly")]
        public async Task<GoalCost> GetMostCostlyGoalFor([FromQuery] DateTime from,
                                                         [FromQuery] DateTime to)
        {
            return await mostCostlyGoalForPeriodQuery.Query(from, to);
        }
    }
}