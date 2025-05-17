using InventoryManagement.Domain.ItemAggregateRoot.ValueObjects;
using InventoryManagement.Domain.StockAggregateRoot.Entities;
using InventoryManagement.Infrastructure.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Configurations;
public class StockItemConfiguration : IEntityTypeConfiguration<StockItem>
{
    public void Configure(EntityTypeBuilder<StockItem> builder)
    {
        builder.ToTable("StockItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnOrder(0)
            .HasColumnName("StockItemId")
            .HasConversion<StockItemIdConverter>()
            .ValueGeneratedNever();

        builder.Property(x => x.ItemId)
            .HasColumnOrder(1)
            .HasColumnName("ItemId")
            .HasConversion<ItemIdConverter>()
            .ValueGeneratedNever();

        builder.Property(x => x.Quantity)
            .HasColumnName("Quantatiy")
            .HasColumnOrder(2);

        builder.Property(x => x.Capacity)
           .HasColumnName("Capacity")
           .HasColumnOrder(3);
    }
}

