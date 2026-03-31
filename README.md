# DependencyInjectionUdemy

A .NET 10 solution focused on Dependency Injection, service lifetimes, assembly scanning, middleware, and endpoint modularization.

## Solution Structure

- `DependencyInjection.ConsoleApp` (Executable)
  - Basic constructor injection demo (`Builder`, `Cekic`, `Civic`).
- `DependencyInjection.WebApplication` (ASP.NET Core Web API)
  - Controllers + minimal APIs
  - Custom middleware (`ExampleMiddleware`)
  - Background service (`LogBackgourndService`)
  - Global exception handling (`ExceptionHandler`)
  - OpenAPI + Scalar
  - Rate limiting, CORS
- `DependencyInjection.Application` (Class Library)
  - Application contracts and commands (`IProductService`, `ProductCreateCommand`).
- `DependencyInjection.Domain` (Class Library)
  - Domain model (`Product`).
- `DependencyInjection.Infrastructure` (Class Library)
  - EF Core InMemory context (`ApplicatoinDbContext`)
  - Service registration via Scrutor (`AddInfrastructure`)
  - `ProductService` implementation.
- `DependencyInjection.MyEndpoints` (Class Library)
  - Endpoint abstraction (`IEndpoint`)
  - Endpoint discovery/registration extensions (`AddMyEndpoints`, `MapMyEndpoints`).

## Assemblies

Default assembly names (same as project names):

- `DependencyInjection.ConsoleApp`
- `DependencyInjection.WebApplication`
- `DependencyInjection.Application`
- `DependencyInjection.Domain`
- `DependencyInjection.Infrastructure`
- `DependencyInjection.MyEndpoints`

Project reference flow:

- `DependencyInjection.WebApplication` → `Application`, `Domain`, `Infrastructure`, `MyEndpoints`
- `DependencyInjection.Infrastructure` → `Application`, `Domain`

Assembly-based behavior used in the project:

- `Infrastructure/AddInfrastructure()` scans **Infrastructure assembly** with Scrutor and registers implementations by interface.
- `MyEndpoints/AddMyEndpoints()` uses `Assembly.GetCallingAssembly()` to find `IEndpoint` implementations in the calling project (WebApplication).
- `WebApplication/AssemblyTest` uses `Assembly.GetExecutingAssembly()` reflection example for `ITest` implementations.

## Requirements

- .NET SDK 10
- Visual Studio 2026 (or any IDE supporting .NET 10)

## Run

### Console app

```bash
dotnet run --project DependencyInjection.ConsoleApp/DependencyInjection.ConsoleApp.csproj
```

### Web API

```bash
dotnet run --project DependencyInjection.WebApplication/DependencyInjection.WebApplication.csproj
```

Default URLs:

- `https://localhost:7205`
- `http://localhost:5048`

## Main Endpoints

- `GET /api/values` → returns `"Ev insa edildi"`
- `GET /test` → returns `"Ev insa edildi"`
- Modular endpoint groups (from modules implementing `IEndpoint`):
  - `/products`
  - `/categories`
  - `/funds`

Example:

```bash
curl http://localhost:5048/api/values
```

## Notes

- `Console.WriteLine` output appears in application output/console, not in HTTP response body.
- For `.http` requests in Visual Studio, ensure the WebApplication project is running first.