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
                ClientName = "React",

                ClientId = "ReactWebClient",
                ClientSecrets = { new Secret("a6a0ece0-0052-4678-82ae-ecc8817d489d".Sha256()) },
                AllowedScopes = {"openid"},
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword
                },
            new Client
            {
                ClientName = "Web Browser Client",

                ClientId = "Browser",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("1554db43-3015-47a8-a748-55bd76b6af48".Sha256()) },
                AllowedScopes = new List<string> {"api1.read", "openid", "profile", "email"}
            },
            new Client
            {
                ClientName = "Catalog",

                ClientId = "CatalogService",
                ClientSecrets = { new Secret("1554db43-3015-47a8-a748-55bd76b6af48".Sha256()) },

                AllowedScopes = new List<string> {"api1.read", "openid", "profile", "email"}
            }

        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope{
                Name = "Catalog API",
                DisplayName = "Catalog"
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
