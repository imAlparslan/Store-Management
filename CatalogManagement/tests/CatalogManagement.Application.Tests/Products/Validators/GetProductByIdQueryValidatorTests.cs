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

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
        }
    }

    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public void Validator_ReturnsValidationError_WhenProductIdInvalid(Guid productId)
    {
        var query = new GetProductByIdQuery(productId);

        var result = validator.Validate(query);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(query.Id)]);
        }
    }
}
