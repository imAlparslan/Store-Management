using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreDefinition.Domain.ShopAggregateRoot.Entities;

namespace StoreDefinition.Infrastructure.Converters;
internal class ShopAddressIdConverter : ValueConverter<ShopAddressId, Guid>
{
    public ShopAddressIdConverter() : base(
            ShopAddressId => ShopAddressId.Value,
            Guid => Guid)
    {
    }
}
