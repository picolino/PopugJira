using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using PopugJira.Accounting.Application.Dtos;
using PopugJira.Accounting.Application.Queries;
using PopugJira.Common;

namespace PopugJira.Accounting.Controllers
{
    [ApiController]
    [Route("api/v1/accinfo")]
    public class AccountingInfoController : ControllerBase
    {
        private readonly IDateTimeService dateTimeService;
        private readonly GetAccountingInfoByDayQuery getAccountingInfoByDayQuery;

        public AccountingInfoController(IDateTimeService dateTimeService,
                                        GetAccountingInfoByDayQuery getAccountingInfoByDayQuery)
        {
            this.dateTimeService = dateTimeService;
            this.getAccountingInfoByDayQuery = getAccountingInfoByDayQuery;
        }

        [HttpGet("today")]
        public async Task<AccountingInfoItemQueryResult[]> GetAccountingInfoForToday() // TODO: Timezones support
        {
            var accountId = User.FindFirst(JwtClaimTypes.Subject)?.Value;
            var todayStart = dateTimeService.Today;
            var todayEnd = dateTimeService.Today.AddDays(1);
            var transactions = await getAccountingInfoByDayQuery.Query(accountId, todayStart, todayEnd);
            return transactions.Select(o => new AccountingInfoItemQueryResult(o)).ToArray();
        }
    }
}