using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

using AcidLabsApi.Frameworks;
using AcidLabsService.Frameworks.ClientsService.ServiceContracts;
using AcidLabsService.Frameworks.ClientsService.Services;
using AcidLabsService.ServiceContracts;
using AcidLabsService.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AcidLabsApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();

            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IAmazonDynamoDbClientExtension, AmazonDynamoDbClientExtension>();
            services.AddTransient<ISecurityAuthService, SecurityAuthService>();

            SecurityAuthService managerService = new SecurityAuthService();

            services.AddAuthentication(o => o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)

                .AddScheme<JwtBearerOptions, JwtBearerHandler>(JwtBearerDefaults.AuthenticationScheme, options => {
                    options.ConfigurationManager = managerService.GetConfiguration();
                    options.ClaimsIssuer = managerService.GetConnectConfiguration().Issuer;
                    options.RequireHttpsMetadata = true;
                    options.Authority = "https://cognito-idp.sa-east-1.amazonaws.com/sa-east-1_VLnPO8inU";
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateLifetime = true,
                        ValidateAudience = false,
                        ValidateIssuer = true
                    };
                });

            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseCustomHandlingException();

            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Acid Labs Technical Test API"));

            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
