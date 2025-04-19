using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.GroupAggregateRoot;

namespace StoreDefinition.Application.Groups.Queries.GetAllGroups;
internal sealed class GetAllGroupsQueryHandler(IGroupRepository groupRepository) : IQueryHandler<GetAllGroupsQuery, Result<IEnumerable<Group>>>
{
    private readonly IGroupRepository groupRepository = groupRepository;
    public async Task<Result<IEnumerable<Group>>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
    {
        var result = await groupRepository.GetAllGroupsAsync(cancellationToken);

        return Result<IEnumerable<Group>>.Success(result);
    }
}
