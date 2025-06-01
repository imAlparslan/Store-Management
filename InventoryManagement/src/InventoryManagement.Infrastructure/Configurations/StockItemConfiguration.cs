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

        builder.ComplexProperty(x => x.Quantity,
            c => c.Property(c => c.Value)
                .ValueGeneratedNever()
                .HasColumnOrder(2)
                .HasColumnName("Quantity"));

        builder.ComplexProperty(x => x.Capacity,
            c => c.Property(c => c.Value)
                .ValueGeneratedNever()
                .HasColumnOrder(3)
                .HasColumnName("Capacity"));

    }
}

