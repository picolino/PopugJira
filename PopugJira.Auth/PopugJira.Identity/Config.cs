﻿using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;

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
                           UserClaims = { 
                                            ClaimTypes.Role, 
                                            ClaimTypes.Name
                                        }
                       }
                   };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
                   {
                       new ApiScope("goal-tracker"),
                       new ApiScope("accounting"),
                       new ApiScope("analytics"),
                       new ApiScope("notifications"),
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
                       },
                       new ApiResource("accounting", "Accounting API")
                       {
                           Scopes = { "accounting" },
                           UserClaims = { ClaimTypes.Role }
                       },
                       new ApiResource("analytics", "Analytics API")
                       {
                           Scopes = { "analytics" },
                           UserClaims = { ClaimTypes.Role }
                       },
                       new ApiResource("notifications", "Notifications API")
                       {
                           Scopes = { "notifications" },
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
                           AllowedScopes = { "openid", "user-info", "goal-tracker", "accounting", "analytics", "notifications" },
                           ClientSecrets =
                           {
                               new Secret("SECRET".Sha512())
                           }
                       }
                   };
        }
    }
}