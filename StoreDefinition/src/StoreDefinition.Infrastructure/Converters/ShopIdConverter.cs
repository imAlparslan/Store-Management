using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Infrastructure.Converters;
internal class ShopIdConverter : ValueConverter<ShopId, Guid>
{
    public ShopIdConverter() : base(
            ShopId => ShopId.Value,
            Guid => Guid)
    {
    }
}
