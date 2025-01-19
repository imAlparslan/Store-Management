using StoreDefinition.Application.Common.Interfaces;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.GroupAggregateRoot;
using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;
using StoreDefinition.SharedKernel;

namespace StoreDefinition.Application.Groups.Commands.CreateGroup;
internal sealed class CreateGroupCommandHandler(IGroupRepository groupRepository) : ICommandHandler<CreateGroupCommand, Result<Group>>
{
    private readonly IGroupRepository groupRepository = groupRepository;
    public async Task<Result<Group>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var groupName = new GroupName(request.Name);
        var groupDescription = new GroupDescription(request.Description);
        var group = new Group(groupName, groupDescription);

        return await groupRepository.InsertGroupAsync(group, cancellationToken);
    }
}
