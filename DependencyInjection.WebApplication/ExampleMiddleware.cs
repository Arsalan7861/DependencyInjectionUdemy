namespace DependencyInjection.WebApplication
{
    public class ExampleMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // context uzerinden gereken islemler

            await next(context); // bir sonraki middleware'e geçer
        }
    }
}
