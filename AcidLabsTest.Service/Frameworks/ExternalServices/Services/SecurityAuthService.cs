using AcidLabsTest.Service.Frameworks.ExternalServices.ServiceContracts;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace AcidLabsTest.Service.Frameworks.ExternalServices.Services
{
    public class SecurityAuthService : ISecurityAuthService
    {
        private readonly string openIdUrl = $"https://cognito-idp.sa-east-1.amazonaws.com/sa-east-1_VLnPO8inU/.well-known/openid-configuration";

        public OpenIdConnectConfiguration GetConnectConfiguration()
        {
            return GetConfiguration().GetConfigurationAsync().GetAwaiter().GetResult();
        }

        public ConfigurationManager<OpenIdConnectConfiguration> GetConfiguration()
        {
            return new ConfigurationManager<OpenIdConnectConfiguration>(openIdUrl, new OpenIdConnectConfigurationRetriever());
        }
    }
}
