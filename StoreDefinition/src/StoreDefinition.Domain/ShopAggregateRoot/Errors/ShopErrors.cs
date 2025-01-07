using StoreDefinition.SharedKernel;

namespace StoreDefinition.Domain.ShopAggregateRoot.Errors;
public class ShopErrors
{
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
}
