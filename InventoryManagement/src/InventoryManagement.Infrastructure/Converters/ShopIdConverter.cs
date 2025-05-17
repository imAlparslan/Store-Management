using InventoryManagement.Domain.StockAggregateRoot.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InventoryManagement.Infrastructure.Converters;
internal class ShopIdConverter : ValueConverter<StockId, Guid>
{
    public ShopIdConverter() : base(
        stockId => stockId.Value,
        guid => guid)
    {
    }
}

