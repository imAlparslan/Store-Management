using InventoryManagement.Domain.ItemAggregateRoot;
using InventoryManagement.Domain.StockAggregateRoot;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InventoryManagement.Infrastructure.Persistence;
public sealed class InventoryDbContext(DbContextOptions<InventoryDbContext> options)
    : DbContext(options)
{
    public DbSet<Stock> Stocks => Set<Stock>();
    public DbSet<Item> Items => Set<Item>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
