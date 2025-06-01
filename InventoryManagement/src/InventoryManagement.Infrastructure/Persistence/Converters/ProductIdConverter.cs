using InventoryManagement.Domain.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InventoryManagement.Infrastructure.Persistence.Converters;
internal class ProductIdConverter : ValueConverter<ProductId, Guid>
{
    public ProductIdConverter() : base(
        productId => productId.Value,
        guid => guid)
    {
    }
}
