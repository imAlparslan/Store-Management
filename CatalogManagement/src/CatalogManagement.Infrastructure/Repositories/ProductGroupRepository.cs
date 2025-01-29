using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;
using CatalogManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CatalogManagement.Infrastructure.Repositories;
internal class ProductGroupRepository(CatalogDbContext catalogDbContext, IUnitOfWorkManager unitOfWorkManager) : IProductGroupRepository
{
    private readonly CatalogDbContext catalogDbContext = catalogDbContext;
    private readonly IUnitOfWorkManager unitOfWorkManager = unitOfWorkManager;

    public async Task<ProductGroup> InsertAsync(ProductGroup productGroup, CancellationToken cancellationToken = default)
    {
        await catalogDbContext.ProductGroups.AddAsync(productGroup, cancellationToken);

        if (!unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await catalogDbContext.SaveChangesAsync(cancellationToken);
        }
        return productGroup;
    }
    public async Task<ProductGroup> UpdateAsync(ProductGroup productGroup, CancellationToken cancellationToken = default)
    {
        catalogDbContext.ProductGroups.Update(productGroup);

        if (!unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await catalogDbContext.SaveChangesAsync(cancellationToken);
        }
        return productGroup;
    }
    public async Task<ProductGroup?> GetByIdAsync(ProductGroupId productGroupId, CancellationToken cancellationToken = default)
    {
        return await catalogDbContext.ProductGroups.FindAsync([productGroupId], cancellationToken);
    }

    public async Task<IEnumerable<ProductGroup>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await catalogDbContext.ProductGroups.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductGroup>> GetProductGroupsByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var res = catalogDbContext.ProductGroups
            .AsEnumerable()
            .Where(x => x.HasProduct(productId));

        return await Task.FromResult(res.ToList());
    }

    public async Task<bool> DeleteByIdAsync(ProductGroupId productGroupId, CancellationToken cancellationToken = default)
    {
        var currentProductGroup = await catalogDbContext.ProductGroups.FindAsync([productGroupId], cancellationToken);

        if (currentProductGroup is null)
        {
            return false;
        }

        catalogDbContext.ProductGroups.Remove(currentProductGroup);

        if (!unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await catalogDbContext.SaveChangesAsync(cancellationToken);
        }
        return true;
    }

    public async Task<bool> IsExistsAsync(ProductGroupId productGroupId, CancellationToken cancellationToken = default)
    {
        return await catalogDbContext.ProductGroups.AnyAsync(x => x.Id == productGroupId, cancellationToken);
    }

    public async Task<List<ProductGroup>> GetByIdsAsync(IReadOnlyList<Guid> groupIds, CancellationToken cancellationToken)
    {
        return await catalogDbContext.ProductGroups
            .AsNoTracking()
            .Where(x => groupIds.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateRangeAsync(List<ProductGroup> groups, CancellationToken cancellationToken)
    {
        catalogDbContext.UpdateRange(groups);

        if (!unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await catalogDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}