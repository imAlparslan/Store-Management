using FluentAssertions;
using StoreDefinition.Domain.Common.Exceptions;
using StoreDefinition.Domain.ShopAggregateRoot.Entities;
using StoreDefinition.Domain.ShopAggregateRoot.Errors;
using StoreDefinition.Domain.ShopAggregateRoot.Exceptions;
using StoreDefinition.Domain.ShopAggregateRoot.ValueObjects;

namespace StoreDefinition.Domain.Tests.ShopAggregate;
public class ShopValueObjectTests
{
    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void CreateingShopDescription_ThrowsShopException_WhenInvalidDescription(string description)
    {
        var action = () => new ShopDescription(description);

        action.Should().ThrowExactly<ShopException>()
            .Which.Should().BeAssignableTo<DomainException>()
            .Which.Code.Should().Be(ShopErrors.InvalidDescription.Code);
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void CreateingShopAddress_ThrowsShopException_WhenInvalidCity(string city)
    {
        string validStreet = "valid street";
        var action = () => new ShopAddress(city, validStreet);

        action.Should().ThrowExactly<ShopException>()
            .Which.Should().BeAssignableTo<DomainException>()
            .Which.Code.Should().Be(ShopErrors.InvalidCity.Code);
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void CreateingShopAddress_ThrowsShopException_WhenInvalidStreet(string street)
    {
        string validCity = "valid city";
        var action = () => new ShopAddress(validCity, street);

        action.Should().ThrowExactly<ShopException>()
            .Which.Should().BeAssignableTo<DomainException>()
            .Which.Code.Should().Be(ShopErrors.InvalidStreet.Code);
    }

    public static readonly TheoryData<string> invalidStrings = ["", " ", null];
}
