using FluentAssertions;
using StoreDefinition.Application.Shops.Queries.GetShopsByGroupId;

namespace StoreDefinition.Application.Tests.Shops.Validations;
public class GetShopsByGroupIdQueryValidatorTests
{
    private readonly GetShopsByGroupIdQueryValidator validator;
    public GetShopsByGroupIdQueryValidatorTests()
    {
        validator = new();
    }

    [Fact]
    public void Validator_ReturnsTrue_WhenQueryValid()
    {
        var query = new GetShopsByGroupIdQuery(Guid.NewGuid());

        var result = validator.Validate(query);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validator_ReturnsFalse_WhenQueryInValid()
    {
        var query = new GetShopsByGroupIdQuery(Guid.Empty);

        var result = validator.Validate(query);

        result.IsValid.Should().BeFalse();
    }
}
