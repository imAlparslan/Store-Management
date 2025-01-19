using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Groups.Queries.GetGroupsByShopId;
internal sealed class GetGroupsByShopIdQueryHandler(IGroupRepository groupRepository) : IQueryHandler<GetGroupsByShopIdQuery, Result<IEnumerable<Group>>>
{
    private readonly IGroupRepository groupRepository = groupRepository;
    public async Task<Result<IEnumerable<Group>>> Handle(GetGroupsByShopIdQuery request, CancellationToken cancellationToken)
    {
        var result = await groupRepository.GetGroupsByShopIdAsync(request.ShopId, cancellationToken);

        return Result<IEnumerable<Group>>.Success(result);
    }
}
