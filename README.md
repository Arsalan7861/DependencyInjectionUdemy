# DependencyInjectionUdemy

A .NET 10 sample solution demonstrating **Dependency Injection (DI)** concepts in both a Console app and an ASP.NET Core Web API.

## Projects

- `DependencyInjection.ConsoleApp`
  - Basic constructor injection example with `Builder`, `Cekic`, and `Civic`.
- `DependencyInjection.WebApplication`
  - DI registrations with different lifetimes (`Transient`, `Scoped`, `Singleton`)
  - Interface mapping (`IBuilder -> Builder`)
  - Custom middleware (`ExampleMiddleware`)
  - Background service (`LogBackgourndService`)
  - Global exception handling (`ExceptionHandler`)
  - CORS, rate limiting, OpenAPI, and controller-based API

## Requirements

- .NET SDK 10
- Visual Studio 2026 (or any IDE that supports .NET 10)

## Run the Console App

From solution root:

```bash
dotnet run --project DependencyInjection.ConsoleApp/DependencyInjection.ConsoleApp.csproj
```

Expected output includes:

- `Cekic kullanildi`
- `Civic kullanildi`
- `Ev insa edildi`

## Run the Web API

From solution root:

```bash
dotnet run --project DependencyInjection.WebApplication/DependencyInjection.WebApplication.csproj
```

Default local URLs (from launch settings):

- `https://localhost:7205`
- `http://localhost:5048`

## API Endpoints

- `GET /api/values`
  - Returns: `"Ev insa edildi"`
- `GET /test`
  - Returns the same DI demo message

Example:

```bash
curl http://localhost:5048/api/values
```

## Notes

- Console logs (`Console.WriteLine`) are written to application output, not HTTP response body.
- For `.http` files in Visual Studio, ensure the WebApplication project is running before sending requests.