using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Controllers;
using WebApplication1.Controllers;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IManagementCars, Car>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, Microsoft.Extensions.Hosting.IHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                var endpoint = context.GetEndpoint();
                if (endpoint != null)
                {
                    var routePattern = (endpoint as ControllerActionDescriptor).AttributeRouteInfo?.Template;
                    var routeData = context.GetRouteData()?.Values;
                    var requestMethod = context.Request.Method;
                    var requestPath = context.Request.Path;

                    // Print information about the incoming request
                    Console.WriteLine($"Incoming request: {requestMethod} {requestPath}");
                    Console.WriteLine($"Route pattern: {routePattern}");
                    Console.WriteLine($"Route data: {string.Join(", ", routeData)}");
                }

                await next();
            });

            app.Map("/cars", carsApp =>
            {
                carsApp.Map("/name", nameApp =>
                {
                    nameApp.Run(async context =>
                    {
                        var managementCars = context.RequestServices.GetService<IManagementCars>();
                        var carName = await managementCars.GetCarName();

                        // Print information about the outgoing response
                        Console.WriteLine($"Outgoing response: {carName}");

                        await context.Response.WriteAsync(carName);
                    });
                });

                carsApp.Map("/engine", engineApp =>
                {
                    engineApp.Run(async context =>
                    {
                        var managementCars = context.RequestServices.GetService<IManagementCars>();
                        var carEngine = await managementCars.GetCarEngine();

                        // Print information about the outgoing response
                        Console.WriteLine($"Outgoing response: {carEngine}");

                        await context.Response.WriteAsync(carEngine);
                    });
                });

                carsApp.Map("/age", ageApp =>
                {
                    ageApp.Run(async context =>
                    {
                        var managementCars = context.RequestServices.GetService<IManagementCars>();
                        var carAge = await managementCars.GetCarAge();

                        // Print information about the outgoing response
                        Console.WriteLine($"Outgoing response: {carAge}");

                        await context.Response.WriteAsync(carAge.ToString());
                    });
                });
            });

            app.UseMvc();
        }
    }
}
