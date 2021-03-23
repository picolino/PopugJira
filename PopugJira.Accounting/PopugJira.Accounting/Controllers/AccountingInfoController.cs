using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopugJira.Accounting.Application.Dtos;
using PopugJira.Accounting.Application.Queries;
using PopugJira.Common;

namespace PopugJira.Accounting.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/accounting")]
    public class AccountingInfoController : ControllerBase
    {
        private readonly IDateTimeService dateTimeService;
        private readonly GetAccountingInfoByPeriodQuery getAccountingInfoByPeriodQuery;
        private readonly GetTopManagementEarnedByPeriodQuery getTopManagementEarnedByPeriodQuery;

        public AccountingInfoController(IDateTimeService dateTimeService,
                                        GetAccountingInfoByPeriodQuery getAccountingInfoByPeriodQuery,
                                        GetTopManagementEarnedByPeriodQuery getTopManagementEarnedByPeriodQuery)
        {
            this.dateTimeService = dateTimeService;
            this.getAccountingInfoByPeriodQuery = getAccountingInfoByPeriodQuery;
            this.getTopManagementEarnedByPeriodQuery = getTopManagementEarnedByPeriodQuery;
        }

        [HttpGet("today")]
        public async Task<AccountingInfoItemQueryResult[]> GetAccountingInfoForToday() // TODO: Timezones support
        {
            var todayStart = dateTimeService.Today;
            var todayEnd = dateTimeService.Today.AddDays(1);
            return await GetAccountingInfoForPeriod(todayStart, todayEnd);
        }
        
        [HttpGet("period")]
        public async Task<AccountingInfoItemQueryResult[]> GetAccountingInfoForPeriod([FromQuery] DateTime from, 
                                                                                      [FromQuery] DateTime to)
        {
            var accountId = User.FindFirst(JwtClaimTypes.Subject)?.Value;
            return await GetAccountingInfoForPeriodByAccount(accountId, from, to);
        }

        [Authorize(Roles = "admin, bookkeeper")]
        [HttpGet("{accountId}/period")]
        public async Task<AccountingInfoItemQueryResult[]> GetAccountingInfoForPeriodByAccount([FromRoute] string accountId,
                                                                                               [FromQuery] DateTime from,
                                                                                               [FromQuery] DateTime to)
        {
            var transactions = await getAccountingInfoByPeriodQuery.Query(accountId, from, to);
            return transactions.Select(o => new AccountingInfoItemQueryResult(o)).ToArray();
        }

        [Authorize(Roles = "admin, bookkeeper")]
        [HttpGet("management/period")]
        public async Task<decimal> GetManagementEarnedForToday([FromQuery] DateTime from,
                                                               [FromQuery] DateTime to)
        {
            var topManagementEarned = await getTopManagementEarnedByPeriodQuery.Query(from, to);
            return topManagementEarned;
        }
    }
}