using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.Domain.GroupAggregateRoot.Errors;

namespace StoreDefinition.Application.Groups.Queries.GetGroupById;
internal sealed class GetGroupByIdQueryHandler(IGroupRepository groupRepository) : IQueryHandler<GetGroupByIdQuery, Result<Group>>
{
    private readonly IGroupRepository groupRepository = groupRepository;
    public async Task<Result<Group>> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await groupRepository.GetGroupByIdAsync(request.GroupId, cancellationToken);

        if (result is null)
        {
            return GroupErrors.NotFoundById;
        }
        return result;
    }
}
