using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PopugJira.Identity.Data;

namespace PopugJira.Identity.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/v1/roles")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        
        [HttpGet("recreate")]
        public async Task<IActionResult> CreateNewRole()
        {
            var roles = new[]
                        {
                            "admin",
                            "programmer",
                            "bookkeeper",
                            "manager"
                        };

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            return Ok();
        }
    }
}