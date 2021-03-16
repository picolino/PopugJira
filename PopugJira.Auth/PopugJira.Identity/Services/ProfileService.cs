using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace PopugJira.Identity.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserStore<IdentityUser> userStore;
        private readonly IRoleStore<IdentityRole> rolesStore;
        private readonly IUserClaimsPrincipalFactory<IdentityUser> claimsFactory;

        public ProfileService(IUserStore<IdentityUser> userStore,
                              IRoleStore<IdentityRole> rolesStore,
                              IUserClaimsPrincipalFactory<IdentityUser> claimsFactory)
        {
            this.userStore = userStore;
            this.rolesStore = rolesStore;
            this.claimsFactory = claimsFactory;
        }

        public virtual async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            if (context.RequestedClaimTypes.Any())
            {
                var user = await userStore.FindByIdAsync(context.Subject.GetSubjectId(), CancellationToken.None);
                if (user != null)
                {
                    var claims = await claimsFactory.CreateAsync(user);
                    var role = context.Subject.FindAll(ClaimTypes.Role);
                
                    context.AddRequestedClaims(role);
                    context.AddRequestedClaims(claims.Claims);
                }
            }
        }

        public virtual async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await userStore.FindByIdAsync(context.Subject.GetSubjectId(), CancellationToken.None);
            context.IsActive = user is not null; 
        }
    }
}