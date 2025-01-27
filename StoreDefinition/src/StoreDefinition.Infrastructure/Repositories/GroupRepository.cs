using Microsoft.EntityFrameworkCore;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;
using StoreDefinition.Infrastructure.Persistence;

namespace StoreDefinition.Infrastructure.Repositories;
public sealed class GroupRepository(StoreDefinitionDbContext context, IUnitOfWorkManager unitOfWorkManager)
    : IGroupRepository
{
    private readonly StoreDefinitionDbContext _context = context;
    private readonly IUnitOfWorkManager _unitOfWorkManager = unitOfWorkManager;
    public async Task<Group> InsertGroupAsync(Group group, CancellationToken cancellation = default)
    {
        await _context.Groups.AddAsync(group, cancellation);
        if (!_unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await _context.SaveChangesAsync(cancellation);
        }
        return group;
    }
    public async Task<Group?> GetGroupByIdAsync(GroupId groupId, CancellationToken cancellation = default)
    {
        return await _context.Groups.FindAsync([groupId], cancellation);
    }

    public async Task<Group> UpdateGroupAsync(Group group, CancellationToken cancellation = default)
    {
        _context.Groups.Update(group);

        if (!_unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await _context.SaveChangesAsync(cancellation);
        }
        return group;
    }
    public async Task<bool> DeleteGroupByIdAsync(GroupId groupId, CancellationToken cancellation = default)
    {
        var group = await _context.Groups.FindAsync([groupId], cancellation);
        if (group is null)
        {
            return false;
        }
        _context.Groups.Remove(group);

        if (!_unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await _context.SaveChangesAsync(cancellation);
        }

        return true;
    }

    public async Task<IEnumerable<Group>> GetAllGroupsAsync(CancellationToken cancellation = default)
    {
        return await _context.Groups.AsNoTracking().ToListAsync(cancellation);
    }


    public async Task<IEnumerable<Group>> GetGroupsByShopIdAsync(ShopId shopId, CancellationToken cancellation = default)
    {
        return await _context.Groups.AsNoTracking()
            .Where(x => x.ShopIds.Contains(shopId))
            .ToListAsync(cancellation);
    }

    public async Task<List<Group>> GetGroupsByIdsAsync(IReadOnlyList<Guid> groupIds, CancellationToken cancellationToken)
    {
        return await _context.Groups.AsNoTracking()
            .Where(x => groupIds.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAll(List<Group> groups, CancellationToken cancellation = default)
    {
        context.Groups.UpdateRange(groups);
        if (!_unitOfWorkManager.IsUnitOfWorkManagerStarted())
        {
            await _context.SaveChangesAsync(cancellation);
        }
    }
}
