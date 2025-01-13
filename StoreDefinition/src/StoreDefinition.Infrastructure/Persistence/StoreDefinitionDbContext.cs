using Microsoft.EntityFrameworkCore;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.Domain.ShopAggregateRoot;
using System.Reflection;
namespace StoreDefinition.Infrastructure.Persistence;
public class StoreDefinitionDbContext(DbContextOptions<StoreDefinitionDbContext> options) : DbContext(options)
{

    public DbSet<Shop> Stores => Set<Shop>();
    public DbSet<Group> Groups => Set<Group>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
