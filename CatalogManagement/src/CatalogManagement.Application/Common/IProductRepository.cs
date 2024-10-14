using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductAggregate.ValueObjects;

namespace CatalogManagement.Application.Common;
public interface IProductRepository
{
    Task<Product> InsertAsync(Product product, CancellationToken cancellationToken = default);
    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);
    Task<bool> IsExistsAsync(ProductId productId, CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsync(ProductId productId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(ProductId productId, CancellationToken cancellationToken = default);
    
}