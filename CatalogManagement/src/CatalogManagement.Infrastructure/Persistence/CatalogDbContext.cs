using CatalogManagement.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CatalogManagement.Infrastructure.Persistence;
public class CatalogDbContext : DbContext
{

    public DbSet<Product> Products => Set<Product>();

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
