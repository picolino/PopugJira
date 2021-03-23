using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopugJira.Accounting.Application.Queries;
using PopugJira.Accounting.Domain;

namespace PopugJira.Accounting.Controllers
{
    [ApiController]
    [Authorize(Roles = "admin, bookkeeper")]
    [Route("api/v1/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly GetAllAccountsQuery getAllAccountsQuery;

        public AccountsController(GetAllAccountsQuery getAllAccountsQuery)
        {
            this.getAllAccountsQuery = getAllAccountsQuery;
        }
        
        [HttpGet]
        public async Task<Account[]> GetAccountList()
        {
            return await getAllAccountsQuery.Query();
        }
    }
}