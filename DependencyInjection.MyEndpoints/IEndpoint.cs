using Microsoft.AspNetCore.Routing;

namespace DependencyInjection.MyEndpoints
{
    public interface IEndpoint
    {
        void MapEndpoints(IEndpointRouteBuilder builder);
    }
}
