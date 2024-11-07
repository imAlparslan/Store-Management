using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductAggregate.ValueObjects;
using CatalogManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CatalogManagement.Infrastructure.Repositories;
internal class ProductRepository : IProductRepository
{
    private readonly CatalogDbContext _catalogDbContext;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    public ProductRepository(CatalogDbContext catalogDbContext, IUnitOfWorkManager unitOfWorkManager)
    {
        _catalogDbContext = catalogDbContext;
        _unitOfWorkManager = unitOfWorkManager;
    }
    public async Task<Product> InsertAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _catalogDbContext.Products.AddAsync(product, cancellationToken);

        if (!_unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await _catalogDbContext.SaveChangesAsync(cancellationToken);
        }
        return product;
    }
    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        _catalogDbContext.Products.Update(product);

        if (!_unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await _catalogDbContext.SaveChangesAsync(cancellationToken);
        }
        return product;
    }
    public async Task<Product?> GetByIdAsync(ProductId productId, CancellationToken cancellationToken = default)
    {
        return await _catalogDbContext.Products.FindAsync(productId, cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _catalogDbContext.Products.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<bool> DeleteByIdAsync(ProductId productId, CancellationToken cancellationToken = default)
    {
        var currentProduct = await _catalogDbContext.Products.FindAsync(productId, cancellationToken);

        if (currentProduct is null)
        {
            return false;
        }

        _catalogDbContext.Products.Remove(currentProduct);

        if (!_unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await _catalogDbContext.SaveChangesAsync(cancellationToken);
        }
        return true;
    }

    public async Task<bool> IsExistsAsync(ProductId productId, CancellationToken cancellationToken = default)
    {
        return await _catalogDbContext.Products.AnyAsync(x => x.Id == productId, cancellationToken);
    }
}