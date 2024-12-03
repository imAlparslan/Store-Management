using CatalogManagement.Application.ProductGroups.Queries.GetProductGroupById;

namespace CatalogManagement.Application.Tests.ProductGroups.Validators;
public class GetProductGroupByIdQuertValidatorTests
{
    [Fact]
    public void Validator_ReturnsValid_WhenProductIdValid()
    {
        var query = new GetProductGroupByIdQuery(Guid.NewGuid());
        var validator = new GetProductGroupByIdQueryValidator();

        var result = validator.Validate(query);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
        }
    }

    [Theory]
    [MemberData(nameof(InvalidGuidData))]
    public void Validator_ReturnsValidationError_WhenProductIdInvalid(Guid productGroupId)
    {
        var query = new GetProductGroupByIdQuery(productGroupId);
        var validator = new GetProductGroupByIdQueryValidator();

        var result = validator.Validate(query);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(query.Id)]);
        }
    }

    public static IEnumerable<object[]> InvalidGuidData => new List<object[]> {
            new object[] { null! },
            new object[] { Guid.Empty },
            new object[] { default(Guid) }
        };
}

