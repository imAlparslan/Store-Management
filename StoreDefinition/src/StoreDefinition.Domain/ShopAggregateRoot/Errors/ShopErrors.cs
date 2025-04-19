namespace StoreDefinition.Domain.ShopAggregateRoot.Errors;
public class ShopErrors
{
    private ShopErrors()
    {

    }
    public static readonly Error InvalidDescription = new Error(
        "Invalid.Shop.ShopDescription",
        ErrorType.Validation,
        "Given shop 'description' is invalid.");

    public static readonly Error InvalidCity = new Error(
       "Invalid.Shop.ShopAddress",
       ErrorType.Validation,
       "Given 'city' is invalid.");

    public static readonly Error InvalidStreet = new Error(
       "Invalid.Shop.ShopAddress",
       ErrorType.Validation,
       "Given 'street' is invalid.");

    public static readonly Error NotFoundById = new Error(
       "Invalid.Shop.NotFoundById",
       ErrorType.NotFound,
       "Shop does not found.");

    public static readonly Error GroupNotAddedToShop = new Error(
       "Invalid.Shop.GroupNotAddedToShop",
       ErrorType.NotUpdated,
       "Group does not added to shop.");

    public static readonly Error GroupNotRemovedFromShop = new Error(
       "Invalid.Shop.GroupNotRemovedFromShop",
       ErrorType.NotUpdated,
       "Group does not removed from shop.");
}
