using System.Threading.RateLimiting;
using DependencyInjection.Domain;
using DependencyInjection.Infrastructure;
using DependencyInjection.Infrastructure.Context;
using DependencyInjection.WebApplication;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Service Collection:
// 1 parça ihtiyaç duyulduğunda hangi classın nereden ve nasıl instance türetileceğini bilgi olarak saklayan bir registration yapısı/container
builder.Services.AddControllers();
builder.Services.AddCors();

builder.Services.AddTransient<Civic>(); // her istek için yeni bir instance üretir
builder.Services.AddSingleton<Cekic>();
builder.Services.AddSingleton<Builder>();
builder.Services.AddScoped<Product>();
builder.Services.AddSingleton<Cekic>(); // tek bir instance üretir ve tüm uygulama boyunca kullanır
builder.Services.AddScoped<Builder>(); // her istek için yeni bir instance üretir, ancak aynı istek içinde aynı instance'ı kullanır

builder.Services.AddScoped<IBuilder, Builder>(); // IBuilder interface'ini Builder class'ına mapler
builder.Services.AddHostedService<LogBackgourndService>(); // BackgroundService'i uygulamaya ekler

builder.Services.AddHttpContextAccessor(); // HttpContextAccessor'ı uygulamaya ekler, böylece HttpContext'e erişim sağlanır

builder.Services.AddInfrastructure(); // Infrastructure katmanındaki servisleri uygulamaya ekler

builder.Services.TryAddTransient<Civic>(); // Try -> Civic class'ının zaten eklenip eklenmediğini kontrol eder, eğer eklenmemişse ekler, eklenmişse eklemez
builder.Services.AddDbContext<ApplicatoinDbContext>();

builder.Services.AddOpenApi();
builder.Services.AddRateLimiter(conf =>
{
    conf.AddFixedWindowLimiter("fixed", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(10);
        opt.PermitLimit = 2;
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });
});

builder.Services.AddTransient<ExampleMiddleware>();
builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails(); // ExceptionHandler'ı uygulamaya ekler, böylece uygulama genelinde oluşan istisnaları yakalayarak işleyebilir ve ProblemDetails formatında yanıt dönebilir
var app = builder.Build();

// Service Provider:
// 2. bu containner'in execute esnasinda o class istenilirse instance tureten mekanizma
// Middleware : uygulama pipeline'ında isteklerin nasıl işleneceğini tanımlayan bir yapıdır. Middleware'ler, gelen istekleri işlemek, yanıtları oluşturmak ve diğer işlemleri gerçekleştirmek için kullanılır. Middleware'ler, uygulamanın farklı katmanlarında yer alabilir ve birbirleriyle etkileşimde bulunabilirler.
// sira onemlidir.
app.MapOpenApi(); // OpenAPI belgelerini oluşturur ve sunar, böylece API'nin nasıl kullanılacağını belgeleyen bir arayüz sağlar
app.MapScalarApiReference();
app.UseHttpsRedirection(); // HTTPS'ye yönlendirme yapar, böylece güvenli bir bağlantı sağlanır
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); // CORS (Cross-Origin Resource Sharing) politikalarını yapılandırır, böylece farklı kaynaklardan gelen isteklere izin verilir
app.UseStaticFiles(); // wwwroot klasöründeki statik dosyaları sunar, böylece CSS, JavaScript ve resim gibi dosyalara erişim sağlanır
app.UseRateLimiter(); // Rate Limiting işlemlerini gerçekleştirir, böylece belirli bir süre içinde belirli sayıda isteği sınırlayarak uygulamanın aşırı yüklenmesini önler

app.UseAuthentication(); // Kimlik doğrulama işlemlerini gerçekleştirir, böylece kullanıcıların kimliklerini doğrulamalarını sağlar
app.UseAuthorization(); // Yetki kontrolu


app.MapGet("/test", () =>
{
    var cekic = app.Services.GetRequiredService<Cekic>();
    var civi = app.Services.GetRequiredService<Civic>();
    var b = new Builder(cekic, civi);
    var message = b.BuilHouse();
    return Results.Ok(message);
});

app.Use((context, next) => // custom middleware, gelen isteği işlemek ve ardından bir sonraki middleware'e geçmek için kullanılır
{
    return next(context); // bir sonraki middleware'e geçer
});
app.UseMiddleware<ExampleMiddleware>(); // ExampleMiddleware'i uygulama pipeline'ına ekler, böylece gelen istekler bu middleware tarafından işlenir
app.UseExceptionHandler();

app.MapControllers();

app.Run();

public class Cekic // dependecy
{
    public void Use()
    {
        Console.WriteLine("Cekic kullanildi");
    }
}

public class Civic // dependecy
{
    public void Use()
    {
        Console.WriteLine("Civic kullanildi");
    }
}

public interface IBuilder
{
    string BuilHouse();
}
public class Builder : IBuilder
{
    Cekic _cekic;
    Civic _civi;

    public Builder(Cekic cekic, Civic civi) // constructor injection
    {
        _cekic = cekic;
        _civi = civi;
    }

    public string BuilHouse()
    {
        _cekic.Use();

        _civi.Use();

        Console.WriteLine("Ev insa edildi");
        return "Ev insa edildi";
    }
}