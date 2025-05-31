using NSubstitute.ReturnsExtensions;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Groups.Queries.GetGroupById;
using StoreDefinition.Application.Tests.Common.Factories.GroupFactories;
using StoreDefinition.Domain.GroupAggregateRoot.Errors;
using StoreDefinition.Domain.GroupAggregateRoot.ValueObjects;

namespace StoreDefinition.Application.Tests.Groups.QueryHandlers;
public class GetGroupByIdQueryHandlerTests
{
    private readonly IGroupRepository groupRepository;
    private readonly GetGroupByIdQueryHandler handler;
    public GetGroupByIdQueryHandlerTests()
    {
        groupRepository = Substitute.For<IGroupRepository>();
        handler = new GetGroupByIdQueryHandler(groupRepository);
    }

    [Fact]
    public async Task Handler_ReturnsGroup_WhenGroupExists()
    {
        var group = GroupFactory.Create();
        groupRepository.GetGroupByIdAsync(group.Id).Returns(group);
        var query = new GetGroupByIdQuery(group.Id);

        var result = await handler.Handle(query, default);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.ShouldBeEquivalentTo(group);
    }

    [Fact]
    public async Task Handler_ReturnsNotFoundByIdError_WhenGroupNotExists()
    {
        groupRepository.GetGroupByIdAsync(Arg.Any<GroupId>()).ReturnsNullForAnyArgs();
        var query = new GetGroupByIdQuery(Guid.NewGuid());

        var result = await handler.Handle(query, default);

        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldNotBeNull();
        result.Errors.ShouldContain(GroupErrors.NotFoundById);
    }
}
