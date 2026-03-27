namespace DependencyInjection.WebApplication
{
    public sealed class LogBackgourndService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                // BackgroundServicete olusturulan nesnelerin yasam turu singleton oldugu icin scope olusturduk ve builder'i aldik. Builder'in bagimliliklarini cozmek icin scope kullaniyoruz.
                var builder = scope.ServiceProvider.GetRequiredService<Builder>();
                builder.BuilHouse();
            }
            return Task.CompletedTask;
        }
    }
}
