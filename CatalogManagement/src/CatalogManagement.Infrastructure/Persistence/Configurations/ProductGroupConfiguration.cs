using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;
using CatalogManagement.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogManagement.Infrastructure.Persistence.Configurations;
internal class ProductGroupConfiguration : IEntityTypeConfiguration<ProductGroup>
{
    public void Configure(EntityTypeBuilder<ProductGroup> builder)
    {
        builder.ToTable("ProductGroup", "Catalog");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
                .HasConversion<ProductGroupIdConverter>()
                .HasColumnName("ProductGroupId")
                .HasColumnOrder(1)
                .ValueGeneratedNever();

        builder.Property("_productIds")
            .HasColumnName("ProductIds")
            .HasColumnOrder(2);

        builder.ComplexProperty<ProductGroupName>(n => n.Name,
            n => n.Property(n => n.Value)
                .ValueGeneratedNever()
                .HasColumnName("Name")
                .HasColumnOrder(3));

        builder.ComplexProperty<ProductGroupDescription>(d => d.Description,
            d => d.Property(d => d.Value)
                .ValueGeneratedNever()
                .HasColumnName("Description")
                .HasColumnOrder(4));

    }
}
