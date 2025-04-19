using FluentAssertions;
using StoreDefinition.Application.Shops.Queries.GetShopById;

namespace StoreDefinition.Application.Tests.Shops.Validations;
public class GetShopByIdQueryValidatorTests
{
    private readonly GetShopByIdQueryValidator _validator;
    public GetShopByIdQueryValidatorTests()
    {
        _validator = new GetShopByIdQueryValidator();
    }


    [Fact]
    public void Validator_ReturnsTrue_WhenQueryValid()
    {
        var query = new GetShopByIdQuery(Guid.NewGuid());

        var result = _validator.Validate(query);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_ReturnsFalse_WhenQueryInvalid()
    {
        var query = new GetShopByIdQuery(Guid.Empty);

        var result = _validator.Validate(query);

        result.IsValid.Should().BeFalse();
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain(nameof(query.ShopId));
    }
}
