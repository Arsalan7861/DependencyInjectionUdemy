using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace DependencyInjection.Infrastructure
{
    public static class ServiceRegistrar
    {
        // extension method -> var olan bir sınıfa yeni bir özellik eklemek için kullanılır. Bu sayede o sınıfın nesneleri üzerinden yeni eklenen özelliklere erişebiliriz.
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //services.AddScoped<IProductService, ProductService>();
            services.Scan(action => action
            .FromAssemblies(typeof(ServiceRegistrar).Assembly) // hangi assembly'den tarama yapacağını belirtir
            .AddClasses(publicOnly: false) // publicOnly: false -> sadece public sınıfları değil, aynı zamanda internal ve private sınıfları da tarar. Bu sayede daha geniş bir sınıf yelpazesini kapsar.
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)// onceden kaydedilmiş bir sınıf varsa, yeni kaydı atlar. Bu sayede aynı sınıfın birden fazla kez kaydedilmesini önler.
            .AsImplementedInterfaces() // taranan sınıfların uyguladığı arayüzleri bulur ve bu arayüzler üzerinden kaydeder. Örneğin, ProductService sınıfı IProductService arayüzünü uyguluyorsa, bu sınıf IProductService arayüzü üzerinden kaydedilir.
            .WithScopedLifetime()
            );

            return services;
        }
    }
}
