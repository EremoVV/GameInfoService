﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            }
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {

        };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {

        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {

        };

        public static List<TestUser> TestUsers => new List<TestUser>
        {

        };

    }
}
