using Microsoft.EntityFrameworkCore;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;
using StoreDefinition.Infrastructure.Persistence;

namespace StoreDefinition.Infrastructure.Repositories;
public sealed class ShopRepository(StoreDefinitionDbContext dbContext, IUnitOfWorkManager unitOfWorkManager)
    : IShopRepository
{
    private readonly StoreDefinitionDbContext _dbContext = dbContext;
    private readonly IUnitOfWorkManager unitOfWorkManager = unitOfWorkManager;

    public async Task<bool> DeleteShopByIdAsync(ShopId shopId, CancellationToken cancellation = default)
    {
        var shop = await _dbContext.Stores.FirstOrDefaultAsync(x => x.Id == shopId, cancellation);
        if (shop is null)
        {
            return false;
        }

        _dbContext.Stores.Remove(shop);

        if (!unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await _dbContext.SaveChangesAsync(cancellation);
        }
        return true;

    }

    public async Task<IEnumerable<Shop>> GetAllShopsAsync(CancellationToken cancellation = default)
    {
        return await _dbContext.Stores.AsNoTracking().ToListAsync(cancellation);
    }


    public async Task<IEnumerable<Shop>> GetShopsByGroupIdAsync(GroupId groupId, CancellationToken cancellation = default)
    {
        return await _dbContext.Stores.AsNoTracking()
            .Where(x => x.GroupIds.Contains(groupId))
            .ToListAsync(cancellation);

    }

    public async Task<Shop> InsertShopAsync(Shop shop, CancellationToken cancellation = default)
    {
        await _dbContext.Stores.AddAsync(shop, cancellation);
        if (!unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await _dbContext.SaveChangesAsync(cancellation);
        }

        return shop;
    }
    public async Task<Shop?> GetShopByIdAsync(ShopId shopId, CancellationToken cancellation = default)
    {
        return await _dbContext.Stores.FindAsync([shopId], cancellation);
    }

    public async Task<Shop> UpdateShopAsync(Shop shop, CancellationToken cancellation = default)
    {
        _dbContext.Stores.Update(shop);

        if (!unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await _dbContext.SaveChangesAsync(cancellation);
        }
        return shop;
    }
}
