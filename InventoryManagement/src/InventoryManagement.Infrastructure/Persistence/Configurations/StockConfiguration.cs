using InventoryManagement.Domain.StockAggregateRoot;
using InventoryManagement.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Persistence.Configurations;
public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable("Stocks")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnOrder(0)
            .HasColumnName("StockId")
            .HasConversion<ShopIdConverter>()
            .ValueGeneratedNever();

        builder.Property(x => x.StoreId)
            .HasColumnOrder(1)
            .HasColumnName("StoreId")
            .HasConversion<StoreIdConverter>()
            .ValueGeneratedNever();

        builder.PrimitiveCollection(x => x.GroupIds)
            .HasColumnName("GroupIds");

        builder.HasMany(x => x.StockItems);
    }
}
