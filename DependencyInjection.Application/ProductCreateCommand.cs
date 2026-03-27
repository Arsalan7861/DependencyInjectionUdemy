using Microsoft.AspNetCore.Http;

namespace DependencyInjection.Application
{
    public sealed class ProductCreateCommand(IProductService productService, IHttpContextAccessor httpContextAccessor)
    {
        public void Handle(string name)
        {
            productService.Create(name);

            //var serv = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IProductService>();
            //serv.Create(name);
        }
    }
}
