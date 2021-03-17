using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PopugJira.EventBus;
using PopugJira.EventBus.Events.UserCud;
using PopugJira.Identity.Models;

namespace PopugJira.Identity.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/v1/accounts")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMessageBus messageBus;

        public AccountController(UserManager<IdentityUser> userManager, IMessageBus messageBus)
        {
            this.userManager = userManager;
            this.messageBus = messageBus;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Login };
                var result = await userManager.CreateAsync(user, model.Password);
                var roleAdd = await userManager.AddToRoleAsync(user, model.Role);
                var claimsAdd = await userManager.AddClaimsAsync(user,
                                                                 new[]
                                                                 {
                                                                     new Claim(ClaimTypes.Name, user.UserName)
                                                                 });
                if (result.Succeeded && roleAdd.Succeeded && claimsAdd.Succeeded)
                {
                    await messageBus.Publish(new UserCreatedEvent
                                             {
                                                 Id = user.Id,
                                                 Name = user.UserName,
                                                 Role = model.Role
                                             });
                    return Ok();
                }

                foreach (var error in result.Errors
                                            .Union(roleAdd.Errors)
                                            .Union(claimsAdd.Errors))
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return BadRequest(model);
        }
    }
}