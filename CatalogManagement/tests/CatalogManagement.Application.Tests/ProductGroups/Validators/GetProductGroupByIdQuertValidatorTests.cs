using CatalogManagement.Application.ProductGroups.Queries.GetProductGroupById;

namespace CatalogManagement.Application.Tests.ProductGroups.Validators;

public class GetProductGroupByIdQueryValidatorTests
{
    private readonly GetProductGroupByIdQueryValidator validator;
    public GetProductGroupByIdQueryValidatorTests()
    {
        validator = new();
    }

    [Fact]
    public void Validator_ReturnsValid_WhenProductIdValid()
    {
        var query = new GetProductGroupByIdQuery(Guid.NewGuid());

        var result = validator.Validate(query);

        result.ShouldSatisfyAllConditions(
            x => result.IsValid.ShouldBeTrue(),
            x => result.Errors.ShouldBeEmpty()
        );
    }

    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public void Validator_ReturnsValidationError_WhenProductIdInvalid(Guid productGroupId)
    {
        var query = new GetProductGroupByIdQuery(productGroupId);

        var result = validator.Validate(query);

        result.ShouldSatisfyAllConditions(
            x => result.IsValid.ShouldBeFalse(),
            x => result.Errors.Select(x => x.PropertyName)
                .ShouldHaveSingleItem(nameof(query.Id))
        );
    }
}

