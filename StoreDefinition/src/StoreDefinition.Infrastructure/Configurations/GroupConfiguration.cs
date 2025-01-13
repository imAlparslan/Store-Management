using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.Infrastructure.Converters;

namespace StoreDefinition.Infrastructure.Configurations;
internal class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("Group");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnOrder(0)
            .HasConversion<GroupIdConverter>()
            .HasColumnName("GroupId")
            .ValueGeneratedNever();

        builder.ComplexProperty(x => x.Name, c =>
           c.Property(v => v.Value)
            .HasColumnName("Name")
            .HasColumnOrder(1)
            .IsRequired());

        builder.ComplexProperty(x => x.Description, c =>
            c.Property(v => v.Value)
             .HasColumnName("Description")
             .HasColumnOrder(2)
             .IsRequired());

        builder.PrimitiveCollection(x => x.ShopIds)
            .HasColumnName("ShopIds");
    }
}
