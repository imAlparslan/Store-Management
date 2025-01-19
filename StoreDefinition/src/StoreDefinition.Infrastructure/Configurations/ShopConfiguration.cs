using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.Infrastructure.Converters;
using StoreDefinition.Infrastructure.Extension;
using System.Collections.Immutable;

namespace StoreDefinition.Infrastructure.Configurations;
internal class ShopConfiguration : IEntityTypeConfiguration<Shop>
{
    public void Configure(EntityTypeBuilder<Shop> builder)
    {
        builder.ToTable("Shop");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnOrder(1)
            .HasConversion<ShopIdConverter>()
            .HasColumnName("ShopId")
            .ValueGeneratedNever();

        builder.ComplexProperty(x => x.Description,
            c => c.Property(x => x.Value)
                .HasColumnOrder(1)
                .HasColumnName("Description")
                .IsRequired());

        builder.PrimitiveCollection(x => x.GroupIds)
            .HasColumnName("GroupIds");

        builder.OwnsOne(x => x.Address, b =>
        {
            b.ToTable("ShopAddress");

            b.HasKey(a => a.Id);

            b.WithOwner().HasForeignKey("ShopId");

            b.Property(x => x.Id)
                .HasColumnOrder(0)
                .HasConversion<ShopAddressIdConverter>()
                .HasColumnName("ShopAddressId")
                .ValueGeneratedNever();

            b.Property(x => x.City)
                .HasColumnOrder(1)
                .HasColumnName("City")
                .IsRequired();

            b.Property(x => x.Street)
                .HasColumnOrder(2)
                .HasColumnName("Street")
                .IsRequired();

        });


    }
}
