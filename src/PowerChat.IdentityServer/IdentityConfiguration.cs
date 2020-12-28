using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace PowerChat.IdentityServer
{
    public static class IdentityConfiguration
    {
        public static IEnumerable<ApiResource> GetApis() => 
            new List<ApiResource>()
            {
                new ApiResource
                {
                    Name = "PowerChatAPI",
                    ApiSecrets = { new Secret("PowerChatAPI".ToSha256()) },
                    Scopes = { "PowerChatAPI" }
                }
            };

        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope>()
            {
                new ApiScope("PowerChatAPI")
            };

        public static IEnumerable<IdentityResource> GetIdentityResources() => 
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>()
            {
                new Client
                {
                    ClientId = "PowerChatMobileClient",
                    ClientSecrets = { new Secret("PowerChatMobileClient".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "PowerChatAPI"
                    },
                    AllowOfflineAccess = true,
                    RequireConsent = false
                }
            };

        public static List<TestUser> GetTestUsers() =>
            new List<TestUser>()
            {
                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "user@test.com",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("email","user@test.com")
                    }
                }
            };
    }
}
