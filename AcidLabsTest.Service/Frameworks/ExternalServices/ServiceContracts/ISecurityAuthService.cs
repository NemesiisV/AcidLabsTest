using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace AcidLabsService.Frameworks.ExternalServices.ServiceContracts
{
    public interface ISecurityAuthService
    {
        ConfigurationManager<OpenIdConnectConfiguration> GetConfiguration();
        OpenIdConnectConfiguration GetConnectConfiguration();
    }
}
