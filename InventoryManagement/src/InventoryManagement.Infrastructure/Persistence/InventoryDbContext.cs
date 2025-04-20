using InventoryManagement.Domain.StockAggregateRoot;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InventoryManagement.Infrastructure.Persistence;
public sealed class InventoryDbContext(DbContextOptions<InventoryDbContext> options)
    : DbContext(options)
{
    public DbSet<Stock> Stocks => Set<Stock>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
