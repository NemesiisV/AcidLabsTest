using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace AcidLabsTest.Service.Frameworks.ExternalServices.ServiceContracts
{
    public interface ISecurityAuthService
    {
        ConfigurationManager<OpenIdConnectConfiguration> GetConfiguration();
        OpenIdConnectConfiguration GetConnectConfiguration();
    }
}
