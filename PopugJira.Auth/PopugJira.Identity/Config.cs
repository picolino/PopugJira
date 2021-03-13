using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace PopugJira.Identity
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
                   {
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResources.Email(),
                       new IdentityResources.Phone(),
                       new IdentityResource
                       {
                           Name = "roles",
                           UserClaims = { ClaimTypes.Role }
                       }
                   };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
                   {
                       new ApiScope("goal-tracker")
                   };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
                   {
                       new ApiResource("goal-tracker", "Goal Tracker API")
                       {
                           Scopes = { "goal-tracker" },
                           UserClaims = { ClaimTypes.Role }
                       }
                   };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
                   {
                       new Client
                       {
                           ClientId = "blazor-client",
                           AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                           AlwaysIncludeUserClaimsInIdToken = true,
                           AllowedScopes = { "openid", "goal-tracker", "roles" },
                           ClientSecrets =
                           {
                               new Secret("SECRET".Sha512())
                           }
                       }
                   };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
                   {
                       new TestUser
                       {
                           SubjectId = "1",
                           Username = "admin",
                           Password = "foobar",
                           Claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Role, "admin")
                                    }
                       },
                       new TestUser
                       {
                           SubjectId = "2",
                           Username = "manager",
                           Password = "foobar",
                           Claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Role, "manager")
                                    },
                       },
                       new TestUser
                       {
                           SubjectId = "3",
                           Username = "programmer",
                           Password = "foobar",
                           Claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Role, "programmer")
                                    },
                       }
                   };
        }
    }
}