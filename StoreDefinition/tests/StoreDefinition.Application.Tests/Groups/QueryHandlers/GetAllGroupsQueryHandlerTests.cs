using FluentAssertions;
using NSubstitute;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Groups.Queries.GetAllGroups;
using StoreDefinition.Application.Tests.Common.Factories.GroupFactories;

namespace StoreDefinition.Application.Tests.Groups.QueryHandlers;
public class GetAllGroupsQueryHandlerTests
{
    private readonly IGroupRepository groupRepository;
    private readonly GetAllGroupsQueryHandler handler;

    public GetAllGroupsQueryHandlerTests()
    {
        groupRepository = Substitute.For<IGroupRepository>();
        handler = new GetAllGroupsQueryHandler(groupRepository);
    }

    [Fact]
    public async Task Handler_ReturnsGroupCollection_WhenGroupsExists()
    {
        var group1 = GroupFactory.Create();
        var group2 = GroupFactory.Create();
        var group3 = GroupFactory.Create();
        groupRepository.GetAllGroupsAsync().Returns([group1, group2, group3]);
        var query = new GetAllGroupsQuery();

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(3);
        result.Value.Should().Contain([group1, group2, group3]);
    }

    [Fact]
    public async Task Handler_ReturnsEmptyCollection_WhenGroupNotExists()
    {
        var query = new GetAllGroupsQuery();
        groupRepository.GetAllGroupsAsync().Returns([]);

        var result = await handler.Handle(query, default); 
        
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeEmpty();
    }
}
