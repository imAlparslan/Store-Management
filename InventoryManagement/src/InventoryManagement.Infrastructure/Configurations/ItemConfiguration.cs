using InventoryManagement.Domain.ItemAggregateRoot;
using InventoryManagement.Infrastructure.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Configurations;
public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Item");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnOrder(0)
            .HasColumnName("ItemId")
            .ValueGeneratedNever()
            .HasConversion<ItemIdConverter>();

        builder.Property(x => x.ProductId)
            .HasColumnOrder(1)
            .HasColumnName("ProductId")
            .ValueGeneratedNever()
            .HasConversion<ProductIdConverter>();

        builder.ComplexProperty(x => x.ProductDefinition, definition =>
        {
            definition.Property(y => y.Code)
                .HasColumnOrder(2)
                .HasColumnName("ProductCode");

            definition.Property(y => y.Name)
                .HasColumnOrder(3)
                .HasColumnName("ProductName");

            definition.Property(y => y.Definition)
                .HasColumnOrder(4)
                .HasColumnName("ProductDefinition");
        });

        builder.Property(x => x.IsDefaultStockItem)
            .HasColumnOrder(5)
            .HasColumnName("IsDefaultStockItem");
    }
}
