using Amazon.Lambda.AspNetCoreServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace AcidLabsApi
{
    public sealed class LambdaEntryPoint : APIGatewayHttpApiV2ProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder.UseStartup<Startup>();
            builder.ConfigureAppConfiguration(config => {
                config.AddJsonFile("Secrets.json");
            });
        }
    }
}
