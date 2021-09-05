using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

using AcidLabsTest.Api.Frameworks;
using AcidLabsTest.Service.Frameworks.ClientsService.ServiceContracts;
using AcidLabsTest.Service.Frameworks.ClientsService.Services;
using AcidLabsTest.Service.ServiceContracts;
using AcidLabsTest.Service.Services;

namespace AcidLabsTest.Api
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
