using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Application.Common.Interfaces;
public interface IShopRepository
{
    Task<Shop> InsertShopAsync(Shop shop, CancellationToken cancellation = default);
    Task<Shop?> GetShopByIdAsync(ShopId shopId, CancellationToken cancellation = default);
    Task<IEnumerable<Shop>> GetAllShopsAsync(CancellationToken cancellation = default);
    Task<Shop?> UpdateShopAsync(Shop shop, CancellationToken cancellation = default);
    Task<IEnumerable<Shop>> GetShopsByGroupIdAsync(GroupId groupId, CancellationToken cancellation = default);
    Task<bool> DeleteShopByIdAsync(ShopId shopId, CancellationToken cancellation = default);
}
