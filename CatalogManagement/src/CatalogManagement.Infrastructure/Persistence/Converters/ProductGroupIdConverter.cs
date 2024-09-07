using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CatalogManagement.Infrastructure.Persistence.Converters;
internal class ProductGroupIdConverter : ValueConverter<ProductGroupId, Guid>
{
    public ProductGroupIdConverter() : base(
        productGroupId => productGroupId,
        guid => guid)
    {
    }
}
