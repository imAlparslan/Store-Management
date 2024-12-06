using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;
using CatalogManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CatalogManagement.Infrastructure.Repositories;
internal class ProductGroupRepository : IProductGroupRepository
{
    private readonly CatalogDbContext _catalogDbContext;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    public ProductGroupRepository(CatalogDbContext catalogDbContext, IUnitOfWorkManager unitOfWorkManager)
    {
        _catalogDbContext = catalogDbContext;
        _unitOfWorkManager = unitOfWorkManager;
    }
    public async Task<ProductGroup> InsertAsync(ProductGroup productGroup, CancellationToken cancellationToken = default)
    {
        await _catalogDbContext.ProductGroups.AddAsync(productGroup, cancellationToken);

        if (!_unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await _catalogDbContext.SaveChangesAsync(cancellationToken);
        }
        return productGroup;
    }
    public async Task<ProductGroup> UpdateAsync(ProductGroup productGroup, CancellationToken cancellationToken = default)
    {
        _catalogDbContext.ProductGroups.Update(productGroup);

        if (!_unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await _catalogDbContext.SaveChangesAsync(cancellationToken);
        }
        return productGroup;
    }
    public async Task<ProductGroup?> GetByIdAsync(ProductGroupId productGroupId, CancellationToken cancellationToken = default)
    {
        return await _catalogDbContext.ProductGroups.FindAsync([productGroupId], cancellationToken);
    }

    public async Task<IEnumerable<ProductGroup>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _catalogDbContext.ProductGroups.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<bool> DeleteByIdAsync(ProductGroupId productGroupId, CancellationToken cancellationToken = default)
    {
        var currentProductGroup = await _catalogDbContext.ProductGroups.FindAsync([productGroupId], cancellationToken);

        if (currentProductGroup is null)
        {
            return false;
        }

        _catalogDbContext.ProductGroups.Remove(currentProductGroup);

        if (!_unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await _catalogDbContext.SaveChangesAsync(cancellationToken);
        }
        return true;
    }

    public async Task<bool> IsExistsAsync(ProductGroupId productGroupId, CancellationToken cancellationToken = default)
    {
        return await _catalogDbContext.ProductGroups.AnyAsync(x => x.Id == productGroupId, cancellationToken);
    }
}