using DependencyInjection.Domain;
using Microsoft.EntityFrameworkCore;

namespace DependencyInjection.Infrastructure.Context;

public sealed class ApplicatoinDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("DependencyInjectionDb");
    }

    public DbSet<Product> Products { get; set; }
}
