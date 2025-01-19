using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Application.Common.Repositories;
public interface IGroupRepository
{
    Task<Group> InsertGroupAsync(Group group, CancellationToken cancellation = default);
    Task<Group?> GetGroupByIdAsync(GroupId groupId, CancellationToken cancellation = default);
    Task<Group> UpdateGroupAsync(Group group, CancellationToken cancellation = default);
    Task<IEnumerable<Group>> GetAllGroupsAsync(CancellationToken cancellation = default);
    Task<IEnumerable<Group>> GetGroupsByShopIdAsync(ShopId shopId, CancellationToken cancellation = default);
    Task<bool> DeleteGroupByIdAsync(GroupId groupId, CancellationToken cancellation = default);
}
