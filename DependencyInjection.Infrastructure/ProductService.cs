using DependencyInjection.Application;
using DependencyInjection.Domain;
using DependencyInjection.Infrastructure.Context;

namespace DependencyInjection.Infrastructure
{
    public sealed class ProductService(ApplicatoinDbContext context) : IProductService
    {
        public void Create(string name)
        {
            context.Products.Add(new Product() { Name = name });
        }
    }
}
