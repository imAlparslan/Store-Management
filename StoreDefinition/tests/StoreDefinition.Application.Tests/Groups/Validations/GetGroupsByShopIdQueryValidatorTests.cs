using StoreDefinition.Application.Groups.Queries.GetGroupsByShopId;

namespace StoreDefinition.Application.Tests.Groups.Validations;

public class GetGroupsByShopIdQueryValidatorTests
{
    private readonly GetGroupsByShopIdQueryValidator validator;
    public GetGroupsByShopIdQueryValidatorTests()
    {
        validator = new GetGroupsByShopIdQueryValidator();
    }

    [Fact]
    public void Validator_ReturnsFalse_WhenQueryValid()
    {
        var query = new GetGroupsByShopIdQuery(Guid.NewGuid());

        var result = validator.Validate(query);

        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validator_ReturnsTrue_WhenQueryInvalid()
    {
        var query = new GetGroupsByShopIdQuery(Guid.Empty);

        var result = validator.Validate(query);

        result.IsValid!.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(query.ShopId));
    }
}
