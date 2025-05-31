using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Domain.ShopAggregateRoot;
using StoreDefinition.Domain.ShopAggregateRoot.Entities;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;
using StoreDefinition.Infrastructure.Tests.Factories;
using StoreDefinition.Infrastructure.Tests.Fixtures;

namespace StoreDefinition.Infrastructure.Tests.RepositoryTests;

public class ShopRepositoryTests : IClassFixture<RepositoryFixture>
{
    private readonly IShopRepository shopRepository;
    public ShopRepositoryTests(RepositoryFixture shopRepositoryFixture)
    {
        shopRepository = shopRepositoryFixture.ShopRepository;

        shopRepositoryFixture.ResetDb();
    }
    [Fact]
    public async Task InsertShopAsync_ReturnsInsertedShop()
    {
        var shop = ShopFactory.CreateValid();

        var insertedShop = await shopRepository.InsertShopAsync(shop);

        insertedShop.ShouldBeEquivalentTo(shop);

    }
    [Fact]
    public async Task GetShopByIdAsync_ReturnsShop()
    {
        var shop = ShopFactory.CreateValid();
        var insertedShop = await shopRepository.InsertShopAsync(shop);

        var shopFromDb = await shopRepository.GetShopByIdAsync(shop.Id);

        shopFromDb.ShouldBeEquivalentTo(insertedShop);
    }

    [Fact]
    public async Task GetShopByIdAsync_WhenShopDoesNotExist_ReturnsNull()
    {
        var shopFromDb = await shopRepository.GetShopByIdAsync(Guid.NewGuid());

        shopFromDb.ShouldBeNull();
    }

    [Fact]
    public async Task UpdateShopAsync_ReturnsUpdatedShop()
    {
        var shop = ShopFactory.CreateValid();
        _ = await shopRepository.InsertShopAsync(shop);
        var newDescription = new ShopDescription("updated description");
        var newAddress = new ShopAddress("updated city", "updated street", shop.Address.Id);
        shop.ChangeDescription(newDescription);
        shop.ChangeAddress(newAddress);

        var updatedShop = await shopRepository.UpdateShopAsync(shop);

        updatedShop.ShouldNotBeNull();
        updatedShop.Description.ShouldBeEquivalentTo(newDescription);
        updatedShop.Address.ShouldBeEquivalentTo(newAddress);
    }

    [Fact]
    public async Task GetAllShopsByGroupIdAsync_ReturnsShops_WhenShopHaveGroupId()
    {
        var groupId = Guid.NewGuid();
        var shopHasGroup1 = ShopFactory.CreateValid();
        shopHasGroup1.AddGroup(groupId);
        var shopHasGroup2 = ShopFactory.CreateValid();
        shopHasGroup2.AddGroup(groupId);
        var shopHasAnotherGroup3 = ShopFactory.CreateValid();
        shopHasAnotherGroup3.AddGroup(Guid.NewGuid());
        var shopWithNoGroup = ShopFactory.CreateValid();
        _ = await shopRepository.InsertShopAsync(shopHasGroup1);
        _ = await shopRepository.InsertShopAsync(shopHasGroup2);
        _ = await shopRepository.InsertShopAsync(shopHasAnotherGroup3);
        _ = await shopRepository.InsertShopAsync(shopWithNoGroup);

        var shopsFromDb = await shopRepository.GetShopsByGroupIdAsync(groupId);

        shopsFromDb.Count().ShouldBe(2);
        shopsFromDb.ShouldBeSubsetOf([shopHasGroup1, shopHasGroup2]);
        shopsFromDb.ShouldNotContain(shopHasAnotherGroup3);
        shopsFromDb.ShouldNotContain(shopWithNoGroup);

    }

    [Fact]
    public async Task GetAllShopsByGroupIdAsync_ReturnsEmptyCollection_WhenGroupNoHaveShopId()
    {
        var shop = ShopFactory.CreateValid();
        shop.AddGroup(Guid.NewGuid());
        var shop2 = ShopFactory.CreateValid();
        _ = await shopRepository.InsertShopAsync(shop);
        _ = await shopRepository.InsertShopAsync(shop2);

        var result = await shopRepository.GetShopsByGroupIdAsync(Guid.NewGuid());

        result.ShouldBeEmpty();
    }

    [Fact]
    public async Task GetAllShopsAsync_ReturnsShopCollection()
    {
        var shop1 = ShopFactory.CreateValid();
        var shop2 = ShopFactory.CreateValid();
        var shop3 = ShopFactory.CreateValid();
        var insertedShop1 = await shopRepository.InsertShopAsync(shop1);
        var insertedShop2 = await shopRepository.InsertShopAsync(shop2);
        var insertedShop3 = await shopRepository.InsertShopAsync(shop3);

        var shopsFromDb = await shopRepository.GetAllShopsAsync();

        shopsFromDb.Count().ShouldBe(3);
        shopsFromDb.ShouldContain(insertedShop1);
        shopsFromDb.ShouldContain(insertedShop2);
        shopsFromDb.ShouldContain(insertedShop3);
    }

    [Fact]
    public async Task GetAllShopsAsync_ReturnsEmptyCollection_WhenShopNotExists()
    {
        var shopsFromDb = await shopRepository.GetAllShopsAsync();

        shopsFromDb.ShouldBeEmpty();
    }

    [Fact]
    public async Task DeleteShopById_ReturnsTrue_WhenShopDeleted()
    {
        var shop = ShopFactory.CreateValid();
        _ = await shopRepository.InsertShopAsync(shop);

        var isDeleted = await shopRepository.DeleteShopByIdAsync(shop.Id);

        isDeleted.ShouldBeTrue();
        var deletedShop = await shopRepository.GetShopByIdAsync(shop.Id);
        deletedShop.ShouldBeNull();

    }

    [Fact]
    public async Task DeleteShopById_ReturnsFalse_WhenShopNotExists()
    {
        var isDeleted = await shopRepository.DeleteShopByIdAsync(Guid.NewGuid());

        isDeleted.ShouldBeFalse();
    }
}