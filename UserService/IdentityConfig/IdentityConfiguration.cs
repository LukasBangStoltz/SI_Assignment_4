using IdentityServer4.Models;

namespace UserService.IdentityConfig
{
    public static class IdentityConfiguration
    {
        #region Identity Config

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("FeedbackService", "FeedbackService Scope"),
                new ApiScope("UserService", "UserService Scope"),
            };

        public static IEnumerable<Client> Clients(IConfiguration configuration)
        {
            var feedbackServiceKey = configuration["FeedbackService:Key"];
            var userServiceKey = configuration["UserService:Key"];


            return new List<Client>
            {
                new Client
                {
                    ClientId = "FeedbackService",
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "FeedbackService" },
                    ClientSecrets = {new Secret(feedbackServiceKey.Sha256())},
                    AllowAccessTokensViaBrowser = true
                },

                 new Client
                {
                    ClientId = "UserService",
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "UserService" },
                    ClientSecrets = {new Secret(userServiceKey.Sha256())},
                    AllowAccessTokensViaBrowser = true
                },
            };
        }

        public static IEnumerable<ApiResource> ApiResources(IConfiguration configuration)
        {

            var feedbackServiceKey = configuration["FeedbackService:Key"];
            var userServiceKey = configuration["UserService:Key"];

            return new List<ApiResource> {
                     new ApiResource
                {
                    Name = "FeedbackService",
                    Description = "FeedbackService resource",
                    ApiSecrets = { new Secret(feedbackServiceKey.Sha256()) },
                    Scopes =  { "FeedbackService", }
                },

                new ApiResource
                {
                    Name = "UserService",
                    Description = "UserService resource",
                    ApiSecrets = { new Secret(userServiceKey.Sha256()) },
                    Scopes =  { "UserService", }
                },

            };
        }
        #endregion

    }
}
