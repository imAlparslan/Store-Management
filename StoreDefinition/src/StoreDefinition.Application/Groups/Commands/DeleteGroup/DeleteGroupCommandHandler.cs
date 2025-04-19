using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.GroupAggregateRoot.Errors;
using StoreDefinition.Domain.GroupAggregateRoot.Events;

namespace StoreDefinition.Application.Groups.Commands.DeleteGroup;
internal sealed class DeleteGroupCommandHandler(IGroupRepository groupRepository) : ICommandHandler<DeleteGroupCommand, Result<bool>>
{
    private readonly IGroupRepository groupRepository = groupRepository;

    public async Task<Result<bool>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await groupRepository.GetGroupByIdAsync(request.GroupId, cancellationToken);
        if (group is null)
        {
            return GroupErrors.NotFoundById;
        }
        group.AddDomainEvent(new GroupDeletedDomainEvent(request.GroupId));
        return await groupRepository.DeleteGroupByIdAsync(request.GroupId, cancellationToken);
    }
}
