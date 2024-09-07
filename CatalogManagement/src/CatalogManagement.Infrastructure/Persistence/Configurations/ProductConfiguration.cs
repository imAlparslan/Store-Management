using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductAggregate.ValueObjects;
using CatalogManagement.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogManagement.Infrastructure.Persistence.Configurations;
internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product", "Catalog");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
                .HasConversion<ProductIdConverter>()
                .HasColumnName("ProductId")
                .HasColumnOrder(1)
                .ValueGeneratedNever();

        builder.ComplexProperty<ProductCode>(p => p.Code,
            c => c.Property(c => c.Value)
                .ValueGeneratedNever()
                .HasColumnOrder(2)
                .HasColumnName("Code"));

        builder.ComplexProperty<ProductName>(p => p.Name,
            n => n.Property(n => n.Value)
                .ValueGeneratedNever()
                .HasColumnOrder(3)
                .HasColumnName("Name"));


        builder.ComplexProperty<ProductDefinition>(p => p.Definition,
            d => d.Property(d => d.Value)
                .ValueGeneratedNever()
                .HasColumnOrder(4)
                .HasColumnName("Definition"));
    }
}
