using CatalogManagement.Application.Products.Queries.GetProductById;

namespace CatalogManagement.Application.Tests.Products.Validators;

public class GetProductByIdQueryValidatorTests
{
    private readonly GetProductByIdQueryValidator validator;
    public GetProductByIdQueryValidatorTests()
    {
        validator = new();
    }

    [Fact]
    public void Validator_ReturnsValid_WhenProductIdValid()
    {
        var query = new GetProductByIdQuery(Guid.NewGuid());

        var result = validator.Validate(query);

        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public void Validator_ReturnsValidationError_WhenProductIdInvalid(Guid productId)
    {
        var query = new GetProductByIdQuery(productId);

        var result = validator.Validate(query);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(query.Id));
    }
}
