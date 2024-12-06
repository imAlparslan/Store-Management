using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;

namespace CatalogManagement.Application.Common.Repositories;
public interface IProductGroupRepository
{
    Task<ProductGroup> InsertAsync(ProductGroup productGroup, CancellationToken cancellationToken = default);
    Task<ProductGroup> UpdateAsync(ProductGroup productGroup, CancellationToken cancellationToken = default);
    Task<bool> IsExistsAsync(ProductGroupId productGroupId, CancellationToken cancellationToken = default);
    Task<ProductGroup?> GetByIdAsync(ProductGroupId productGroupId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProductGroup>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(ProductGroupId productGroupId, CancellationToken cancellationToken = default);

}
