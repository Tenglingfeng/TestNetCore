using System.Collections.Generic;
using IdentityServer4.Models;

namespace Applications.WebApi.Config
{
    /// <summary>
    /// IdentityServer 配置文件
    /// </summary>
    public static class IdentityServerConfig
    {
        /// <summary>
        /// scope
        /// </summary>
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("scope1"),
            };

        /// <summary>
        /// 受保护的api资源
        /// </summary>
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("api1","#api1")
                {
                    //!!!重要
                    Scopes = { "scope1"}
                },
            };

        /// <summary>
        /// 客户端
        /// </summary>
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "scope1" }
                },
            };
    }
}