using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using PopugJira.Accounting.Application.Dtos;
using PopugJira.Accounting.Application.Queries;
using PopugJira.Common;

namespace PopugJira.Accounting.Controllers
{
    // TODO: Authorization
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
            var accountId = User.FindFirst(JwtClaimTypes.Subject)?.Value;
            var todayStart = dateTimeService.Today;
            var todayEnd = dateTimeService.Today.AddDays(1);
            var transactions = await getAccountingInfoByPeriodQuery.Query(accountId, todayStart, todayEnd);
            return transactions.Select(o => new AccountingInfoItemQueryResult(o)).ToArray();
        }
        
        [HttpGet("today/management")]
        public async Task<decimal> GetManagementEarnedForToday() // TODO: Timezones support
        {
            var todayStart = dateTimeService.Today;
            var todayEnd = dateTimeService.Today.AddDays(1);
            var topManagementEarned = await getTopManagementEarnedByPeriodQuery.Query(todayStart, todayEnd);
            return topManagementEarned;
        }
    }
}