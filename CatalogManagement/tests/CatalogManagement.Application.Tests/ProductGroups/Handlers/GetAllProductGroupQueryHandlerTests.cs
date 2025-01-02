﻿using CatalogManagement.Domain.ProductGroupAggregate;

namespace CatalogManagement.Application.Tests.ProductGroups.Handlers;
public class GetAllProductGroupQueryHandlerTests
{
    [Fact]
    public async Task Handler_ReturnsProductGroupList_WhenProductsExist()
    {
        var productGroups = new List<ProductGroup>() { ProductGroupFactory.CreateRandom(), ProductGroupFactory.CreateRandom() };
        var command = new GetAllProductGroupsQuery();
        var productGroupRepository = Substitute.For<IProductGroupRepository>();
        productGroupRepository.GetAllAsync().Returns(productGroups);
        var handler = new GetAllProductGroupsQueryHandler(productGroupRepository);

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Contain(productGroups);
            result.Errors.Should().BeNullOrEmpty();
        }
    }

    [Fact]
    public async Task Handler_ReturnsEmptyList_WhenNoProductGroupsExist()
    {
        var command = new GetAllProductGroupsQuery();
        var productGroupRepository = Substitute.For<IProductGroupRepository>();
        productGroupRepository.GetAllAsync().Returns(Enumerable.Empty<ProductGroup>());
        var handler = new GetAllProductGroupsQueryHandler(productGroupRepository);

        var result = await handler.Handle(command, default);

        using (AssertionScope scope = new())
        {
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(Enumerable.Empty<ProductGroup>());
            result.Errors.Should().BeNullOrEmpty();
        }
    }
}
