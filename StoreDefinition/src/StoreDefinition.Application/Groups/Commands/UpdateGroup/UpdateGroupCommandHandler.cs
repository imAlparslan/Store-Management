using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.Domain.GroupAggregateRoot.Errors;
using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;

namespace StoreDefinition.Application.Groups.Commands.UpdateGroup;
internal sealed class UpdateGroupCommandHandler(IGroupRepository groupRepository) : ICommandHandler<UpdateGroupCommand, Result<Group>>
{
    private readonly IGroupRepository groupRepository = groupRepository;
    public async Task<Result<Group>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await groupRepository.GetGroupByIdAsync(request.GroupId, cancellationToken);

        if (group is null)
        {
            return GroupErrors.NotFoundById;
        }

        group.ChangeName(new GroupName(request.Name));
        group.ChangeDescription(new GroupDescription(request.Description));

        return await groupRepository.UpdateGroupAsync(group, cancellationToken);
    }
}
