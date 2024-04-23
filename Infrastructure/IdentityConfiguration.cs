using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace AutoMail.Infrastructure
{
    public class IdentityConfiguration
    {
        public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "api1" }
            }
        };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
            new ApiScope("api1", "My API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
            };

        public static List<TestUser> Users =>
            new List<TestUser>
            {
            new TestUser
            {
                SubjectId = "1",
                Username = "alice",
                Password = "password",
                Claims = new List<Claim>
                {
                    new Claim("name", "Alice"),
                    new Claim("website", "https://alice.com")
                }
            }
            };
    }
}
