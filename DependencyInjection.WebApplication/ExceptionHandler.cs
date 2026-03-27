using Microsoft.AspNetCore.Diagnostics;

namespace DependencyInjection.WebApplication
{
    public class ExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var res = new
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace
            };
            await httpContext.Response.WriteAsJsonAsync(res, cancellationToken);
            return true;
        }
    }
}
