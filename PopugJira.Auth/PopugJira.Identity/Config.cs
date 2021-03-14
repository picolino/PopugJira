using System;
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
                           Name = "user-info",
                           UserClaims = { "id", ClaimTypes.Role, ClaimTypes.Name }
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
                           UserClaims = { "id", ClaimTypes.Role }
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
                           AllowedScopes = { "openid", "goal-tracker", "user-info" },
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
                                        new Claim("id", "337bce1b-2450-4a62-a5de-3af99a773e1d"),
                                        new Claim(ClaimTypes.Name, "Administrator"),
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
                                        new Claim("id", "745a27f1-4c96-46fc-b6fd-c88b190ce96b"),
                                        new Claim(ClaimTypes.Name, "Manager"),
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
                                        new Claim("id", "3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                                        new Claim(ClaimTypes.Name, "Programmer"),
                                        new Claim(ClaimTypes.Role, "programmer")
                                    },
                       }
                   };
        }
    }
}