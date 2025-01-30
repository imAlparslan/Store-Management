using StoreDefinition.Contracts.Shops;

namespace StoreDefinition.Api.Tests.Factories;
public static class ShopsRequestFactory
{

    public static CreateShopRequest CreateShopCreateRequest(
        string shopDescription = "shop description",
        string city = "shop address city",
        string street = "shop address street")
    {
        return new CreateShopRequest(
                shopDescription,
                city,
                street,
                []);
    }

    public static UpdateShopRequest UpdateShopCreateRequest(
        string shopDescription = "update shop description",
        string city = "update shop address city",
        string street = "update shop address street")
    {
        return new UpdateShopRequest(
                shopDescription,
                city,
                street);
    }
    public static AddGroupToShopRequest CreateAddGroupToShopRequest(Guid groupId)
    {
        return new AddGroupToShopRequest(groupId);
    }

    public static RemoveGroupFromShopRequest CreateRemoveGroupFromShopRequest(Guid groupId)
    {
        return new RemoveGroupFromShopRequest(groupId);
    }
}
