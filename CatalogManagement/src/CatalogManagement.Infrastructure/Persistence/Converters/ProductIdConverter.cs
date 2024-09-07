using CatalogManagement.Domain.ProductAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CatalogManagement.Infrastructure.Persistence.Converters;
internal class ProductIdConverter : ValueConverter<ProductId, Guid>
{
    public ProductIdConverter() : base(
        productId => productId.Value,
        Guid => Guid)
    {
    }
}
