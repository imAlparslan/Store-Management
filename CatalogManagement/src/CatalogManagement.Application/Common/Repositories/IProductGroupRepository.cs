using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;

namespace CatalogManagement.Application.Common.Repositories;
public interface IProductGroupRepository
{
    Task<ProductGroup> InsertAsync(ProductGroup product, CancellationToken cancellationToken = default);
    Task<ProductGroup> UpdateAsync(ProductGroup product, CancellationToken cancellationToken = default);
    Task<bool> IsExistsAsync(ProductGroupId productId, CancellationToken cancellationToken = default);
    Task<ProductGroup?> GetByIdAsync(ProductGroupId productId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProductGroup>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(ProductGroupId productId, CancellationToken cancellationToken = default);

}
