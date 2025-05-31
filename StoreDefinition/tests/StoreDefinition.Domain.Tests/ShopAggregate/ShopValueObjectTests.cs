using StoreDefinition.Domain.ShopAggregateRoot.Entities;
using StoreDefinition.Domain.ShopAggregateRoot.Errors;
using StoreDefinition.Domain.ShopAggregateRoot.Exceptions;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Domain.Tests.ShopAggregate;

public class ShopValueObjectTests
{
    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public void CreatingShopDescription_ThrowsShopException_WhenInvalidDescription(string description)
    {
        var action = () => new ShopDescription(description);

        action.ShouldThrow<ShopException>()
            .ShouldSatisfyAllConditions(
                ex => ex.Code.ShouldBe(ShopErrors.InvalidDescription.Code),
                ex => ex.Message.ShouldBe(ShopErrors.InvalidDescription.Description),
                ex => ex.ShouldBeAssignableTo<DomainException>()
            );
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public void CreatingShopAddress_ThrowsShopException_WhenInvalidCity(string city)
    {
        string validStreet = "valid street";
        var action = () => new ShopAddress(city, validStreet);

        action.ShouldThrow<ShopException>()
            .ShouldSatisfyAllConditions(
                ex => ex.Code.ShouldBe(ShopErrors.InvalidCity.Code),
                ex => ex.Message.ShouldBe(ShopErrors.InvalidCity.Description),
                ex => ex.ShouldBeAssignableTo<DomainException>()
            );
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public void CreatingShopAddress_ThrowsShopException_WhenInvalidStreet(string street)
    {
        string validCity = "valid city";
        var action = () => new ShopAddress(validCity, street);

        action.ShouldThrow<ShopException>()
            .ShouldSatisfyAllConditions(
                ex => ex.Code.ShouldBe(ShopErrors.InvalidStreet.Code),
                ex => ex.Message.ShouldBe(ShopErrors.InvalidStreet.Description)
            );
    }
}