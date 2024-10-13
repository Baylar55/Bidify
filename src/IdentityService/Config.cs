using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("auctionApp", "Auction app full access"),
        };

    public static IEnumerable<Client> Clients =>
        [
            new Client{
                ClientId = "postman",
                ClientName = "Postman",
                AllowedScopes = {"openid", "profile", "auctionApp"},
                RedirectUris = {"https://www.getpostman.com/oauth2/callback"},
                ClientSecrets = [new Secret("NotASecret".Sha256())],
                AllowedGrantTypes = {GrantType.ResourceOwnerPassword}
            },
            new Client{
                ClientId = "nextApp",
                ClientName = "nextApp",
                RequirePkce = false,
                AllowOfflineAccess =true,
                AccessTokenLifetime = 3600*24*30,
                ClientSecrets = [new Secret("secret".Sha256())],
                AllowedScopes = {"openid", "profile", "auctionApp"},
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                RedirectUris = {"http://localhost:3000/api/auth/callback/id-server"},

            }
        ];
}
