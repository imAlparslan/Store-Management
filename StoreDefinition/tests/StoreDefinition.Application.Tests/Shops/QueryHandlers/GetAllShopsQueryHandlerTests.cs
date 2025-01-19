﻿using FluentAssertions;
using NSubstitute;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Shops.Queries.GetAllShops;
using StoreDefinition.Application.Tests.Common.Factories.ShopFactories;
using StoreDefinition.Domain.ShopAggregateRoot;

namespace StoreDefinition.Application.Tests.Shops.QueryHandlers;
public class GetAllShopsQueryHandlerTests
{
    private readonly IShopRepository _shopRepository;
    private readonly GetAllShopsQuery _query = new GetAllShopsQuery();
    private readonly GetAllShopsQueryHandler _handler;
    public GetAllShopsQueryHandlerTests()
    {
        _shopRepository = Substitute.For<IShopRepository>();
        _handler = new GetAllShopsQueryHandler(_shopRepository);
    }

    [Fact]
    public async Task Handler_ReturnsShopCollection_WhenShopsExists()
    {
        _shopRepository.GetAllShopsAsync().Returns(
            [ShopFactory.CreateValid(),
             ShopFactory.CreateValid()]);

        var result = await _handler.Handle(_query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNullOrEmpty();
        result.Value.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handler_ReturnsEmptyShopCollection_WhenNoShopsExists()
    {
        _shopRepository.GetAllShopsAsync().Returns([]);

        var result = await _handler.Handle(_query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeEmpty();
    }
}
