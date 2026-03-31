using System.Reflection;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.MyEndpoints
{
    public static class Extensions
    {
        public static void AddMyEndpoints(this IServiceCollection services)
        {
            var assembly = Assembly.GetCallingAssembly(); // Get the assembly that called this method
            //var assembly = Assembly.GetEntryAssembly(); // Get the asssembly that contains the entry point of the application
            var types = assembly.GetTypes()
                .Where(i => i.IsClass
                && !i.IsAbstract
                && typeof(IEndpoint).IsAssignableFrom(i)
                );
            foreach (var type in types)
            {
                services.AddTransient(typeof(IEndpoint), type);
            }
        }

        public static void MapMyEndpoints(this IEndpointRouteBuilder builder)
        { // Get the services of type IEndpoint from the service provider and call their MapEndpoints method to map the endpoints to the application
            var endpoints = builder.ServiceProvider.GetServices<IEndpoint>();
            foreach (var endpoint in endpoints)
            {
                endpoint.MapEndpoints(builder);
            }
        }
    }
}
