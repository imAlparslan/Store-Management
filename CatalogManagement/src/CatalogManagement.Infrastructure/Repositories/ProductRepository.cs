using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductAggregate.ValueObjects;
using CatalogManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CatalogManagement.Infrastructure.Repositories;
internal class ProductRepository(CatalogDbContext catalogDbContext, IUnitOfWorkManager unitOfWorkManager) : IProductRepository
{
    private readonly CatalogDbContext catalogDbContext = catalogDbContext;
    private readonly IUnitOfWorkManager unitOfWorkManager = unitOfWorkManager;

    public async Task<Product> InsertAsync(Product product, CancellationToken cancellationToken = default)
    {
        await catalogDbContext.Products.AddAsync(product, cancellationToken);

        if (!unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await catalogDbContext.SaveChangesAsync(cancellationToken);
        }
        return product;
    }
    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        catalogDbContext.Products.Update(product);

        if (!unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await catalogDbContext.SaveChangesAsync(cancellationToken);
        }
        return product;
    }
    public async Task<Product?> GetByIdAsync(ProductId productId, CancellationToken cancellationToken = default)
    {
        return await catalogDbContext.Products.FindAsync([productId], cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await catalogDbContext.Products.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<bool> DeleteByIdAsync(ProductId productId, CancellationToken cancellationToken = default)
    {
        var currentProduct = await catalogDbContext.Products.FindAsync([productId], cancellationToken);

        if (currentProduct is null)
        {
            return false;
        }

        catalogDbContext.Products.Remove(currentProduct);

        if (!unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await catalogDbContext.SaveChangesAsync(cancellationToken);
        }
        return true;
    }

    public async Task<bool> IsExistsAsync(ProductId productId, CancellationToken cancellationToken = default)
    {
        return await catalogDbContext.Products.AnyAsync(x => x.Id == productId, cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetByGroupAsync(Guid groupId, CancellationToken cancellationToken = default)
    {
        var set = catalogDbContext.Products
            .AsEnumerable()
            .Where(x => x.GroupIds.Contains(groupId));

        return await Task.FromResult(set.ToList());

    }
}