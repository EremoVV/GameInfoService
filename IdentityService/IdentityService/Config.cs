using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityService
{
    public static class Config
    {
        public static IEnumerable<Client> Clients => new Client[]
        {
            new Client
            {
                ClientId = "ReactClient",
                ClientSecrets =
                {
                    new Secret("ReactClientSecret".Sha256())
                },
                AllowedScopes = {},
                AllowedGrantTypes =
                {
                    GrantType.ClientCredentials
                }
            },
            new Client
            {
                ClientName = "Client Application2",
                ClientId = "Browser",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("1554db43-3015-47a8-a748-55bd76b6af48".Sha256()) },
                AllowedScopes = new List<string> {"api1.read", "openid", "profile", "email"}
            }

        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope
            {
                Name = "api1.read"
            }
        };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {

        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.Profile(),
            new IdentityResources.OpenId(),
            new IdentityResources.Email()
        };

        public static List<TestUser> TestUsers => new List<TestUser>
        {

        };

    }
}
