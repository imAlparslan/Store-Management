﻿using CatalogManagement.Domain.ProductGroupAggregate;

namespace CatalogManagement.Application.Tests.ProductGroups.Handlers;

public class GetAllProductGroupQueryHandlerTests
{
    private readonly IProductGroupRepository productGroupRepository;
    private readonly GetAllProductGroupsQueryHandler handler;
    public GetAllProductGroupQueryHandlerTests()
    {
        productGroupRepository = Substitute.For<IProductGroupRepository>();
        handler = new GetAllProductGroupsQueryHandler(productGroupRepository);
    }

    [Fact]
    public async Task Handler_ReturnsProductGroupList_WhenProductsExist()
    {
        var productGroups = new List<ProductGroup>() { ProductGroupFactory.CreateRandom(), ProductGroupFactory.CreateRandom() };
        var command = new GetAllProductGroupsQuery();
        productGroupRepository.GetAllAsync().Returns(productGroups);

        var result = await handler.Handle(command, default);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.ShouldBeSubsetOf(productGroups);
        result.Errors.ShouldBeNull();
    }

    [Fact]
    public async Task Handler_ReturnsEmptyList_WhenNoProductGroupsExist()
    {
        var command = new GetAllProductGroupsQuery();
        productGroupRepository.GetAllAsync().Returns(Enumerable.Empty<ProductGroup>());

        var result = await handler.Handle(command, default);

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEmpty();
        result.Errors.ShouldBeNull();
    }
}
