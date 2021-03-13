using System.Collections.Generic;
using System.Linq;
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
                       new IdentityResources.Phone()
                   };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
                   {
                       new ApiScope("default-api-scope")
                   };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
                   {
                       new ApiResource("default-api-resource", "Default (all) API")
                       {
                           Scopes = { "default-api-scope" }
                       }
                   };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
                   {
                       new Client
                       {
                           ClientId = "client",
                           AllowedGrantTypes = GrantTypes.ClientCredentials.Union(GrantTypes.ResourceOwnerPassword).ToList(),
                           AllowedScopes = {"default-api-scope"},
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
                           Username = "foo",
                           Password = "bar",
                           IsActive = true
                       }
                   };
        }
    }
}