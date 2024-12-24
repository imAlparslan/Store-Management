using CatalogManagement.Domain.ProductGroupAggregate;

namespace CatalogManagement.Application.Tests.TestDoubles;
public class InMemoryProductGroupRepository : Dictionary<Guid, ProductGroup>, IProductGroupRepository
{
    public Task<bool> DeleteByIdAsync(ProductGroupId productGroupId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Remove(productGroupId));
    }

    public async Task<IEnumerable<ProductGroup>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var values = Values.ToArray();
        return await Task.FromResult(values);
    }

    public async Task<ProductGroup?> GetByIdAsync(ProductGroupId productGroupId, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(base[productGroupId]);
    }

    public async Task<IEnumerable<ProductGroup>> GetProductGroupsByContainigProduct(Guid productId, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(Values.Where(x => x.ProductIds.Contains(productId)));
    }

    public Task<ProductGroup> InsertAsync(ProductGroup productGroup, CancellationToken cancellationToken = default)
    {
        Add(productGroup.Id, productGroup);
        return Task.FromResult(productGroup);
    }

    public Task<bool> IsExistsAsync(ProductGroupId productGroupId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(TryGetValue(productGroupId, out var result));
    }

    public Task<ProductGroup> UpdateAsync(ProductGroup productGroup, CancellationToken cancellationToken = default)
    {
        base[productGroup.Id] = productGroup;

        return Task.FromResult(productGroup);
    }
}

