﻿using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Infrastructure.Tests.Factories;
using StoreDefinition.Infrastructure.Tests.Fixtures;

namespace StoreDefinition.Infrastructure.Tests.RepositoryTests;
public class GroupRepositoryTests : IClassFixture<RepositoryFixture>
{
    private readonly IGroupRepository groupRepository;

    public GroupRepositoryTests(RepositoryFixture repositoryFixture)
    {
        groupRepository = repositoryFixture.GroupRepository;

        repositoryFixture.ResetDb();
    }

    [Fact]
    public async Task InsertGroupAsync_ReturnsInsertedGroup()
    {
        var group = GroupFactory.CreateValid();

        var result = await groupRepository.InsertGroupAsync(group);

        result.ShouldBeEquivalentTo(group);
    }

    [Fact]
    public async Task GetGroupById_ReturnsGroup_WhenGroupExists()
    {
        var group = GroupFactory.CreateValid();
        var group2 = GroupFactory.CreateValid();
        _ = await groupRepository.InsertGroupAsync(group);
        _ = await groupRepository.InsertGroupAsync(group2);

        var result = await groupRepository.GetGroupByIdAsync(group.Id);

        result.ShouldNotBeNull();
        result.ShouldBeEquivalentTo(group);

    }

    [Fact]
    public async Task GetGroupById_ReturnsNull_WhenGroupNotExists()
    {
        var group = GroupFactory.CreateValid();
        _ = await groupRepository.InsertGroupAsync(group);

        var result = await groupRepository.GetGroupByIdAsync(Guid.NewGuid());

        result.ShouldBeNull();
    }

    [Fact]
    public async Task UpdateGroupAsync_ReturnsUpdatedGroup_WhenGroupExists()
    {
        var updateName = GroupNameFactory.CreateCustom("Updated group name");
        var updateDescription = GroupDescriptionFactory.CreateCustom("Updated group description");
        var group = GroupFactory.CreateValid();
        _ = await groupRepository.InsertGroupAsync(group);

        group.ChangeName(updateName);
        group.ChangeDescription(updateDescription);

        var updatedGroup = await groupRepository.UpdateGroupAsync(group);

        updatedGroup.ShouldNotBeNull();
        updatedGroup.ShouldBeEquivalentTo(group);
        updatedGroup.ShouldBeEquivalentTo(await groupRepository.GetGroupByIdAsync(group.Id));

    }

    [Fact]
    public async Task DeleteGroupByIdAsync_ReturnsTrue_WhenGroupDeleted()
    {
        var group = GroupFactory.CreateValid();
        _ = await groupRepository.InsertGroupAsync(group);

        var result = await groupRepository.DeleteGroupByIdAsync(group.Id);

        result.ShouldBeTrue();
    }
    [Fact]
    public async Task DeleteGroupByIdAsync_ReturnsFalse_WhenGroupNotExists()
    {
        var group = GroupFactory.CreateValid();

        var result = await groupRepository.DeleteGroupByIdAsync(group.Id);

        result.ShouldBeFalse();
    }

    [Fact]
    public async Task GetAllGroupsAsync_ReturnsAllGroups_WhenGroupsExists()
    {
        var group = GroupFactory.CreateValid();
        var group2 = GroupFactory.CreateValid();
        var group3 = GroupFactory.CreateValid();
        var inserted = await groupRepository.InsertGroupAsync(group);
        var inserted2 = await groupRepository.InsertGroupAsync(group2);
        var inserted3 = await groupRepository.InsertGroupAsync(group3);

        var result = await groupRepository.GetAllGroupsAsync();

        result.ShouldNotBeEmpty();
        result.Count().ShouldBe(3);
        result.ShouldBeSubsetOf([inserted, inserted2, inserted3]);
    }

    [Fact]
    public async Task GetAlLGroupsAsync_ReturnsEmptyCollection_WhenNoGroupExists()
    {
        var result = await groupRepository.GetAllGroupsAsync();

        result.ShouldBeEmpty();
    }

    [Fact]
    public async Task GetGroupsByShopIdAsync_ReturnsGroups_WhenGroupHaveShopId()
    {
        var shopId = Guid.NewGuid();
        var groupHaveShopId1 = GroupFactory.CreateValid();
        groupHaveShopId1.AddShop(shopId);
        var groupHaveShopId2 = GroupFactory.CreateValid();
        groupHaveShopId2.AddShop(shopId);
        var groupHaveAnotherShopId = GroupFactory.CreateValid();
        groupHaveAnotherShopId.AddShop(Guid.NewGuid());
        var groupHaveNoShopId = GroupFactory.CreateValid();
        _ = await groupRepository.InsertGroupAsync(groupHaveShopId1);
        _ = await groupRepository.InsertGroupAsync(groupHaveShopId2);
        _ = await groupRepository.InsertGroupAsync(groupHaveAnotherShopId);
        _ = await groupRepository.InsertGroupAsync(groupHaveNoShopId);

        var result = await groupRepository.GetGroupsByShopIdAsync(shopId);

        result.Count().ShouldBe(2);
        result.ShouldBeSubsetOf([groupHaveShopId1, groupHaveShopId2]);
        result.ShouldNotContain(groupHaveAnotherShopId);
        result.ShouldNotContain(groupHaveNoShopId);
    }

    [Fact]
    public async Task GetGroupsByShopIdAsync_ReturnsEmptyCollection_WhenGroupHaveNoShopId()
    {
        var shopId = Guid.NewGuid();
        var groupHaveAnotherShopId = GroupFactory.CreateValid();
        groupHaveAnotherShopId.AddShop(Guid.NewGuid());
        var groupHaveNoShopId = GroupFactory.CreateValid();

        _ = await groupRepository.InsertGroupAsync(groupHaveAnotherShopId);
        _ = await groupRepository.InsertGroupAsync(groupHaveNoShopId);

        var result = await groupRepository.GetGroupsByShopIdAsync(shopId);

        result.ShouldBeEmpty();
    }
}
