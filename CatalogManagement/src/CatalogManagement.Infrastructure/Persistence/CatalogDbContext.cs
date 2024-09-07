using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductGroupAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CatalogManagement.Infrastructure.Persistence;
public class CatalogDbContext : DbContext
{

    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductGroup> ProductGroups => Set<ProductGroup>();

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
