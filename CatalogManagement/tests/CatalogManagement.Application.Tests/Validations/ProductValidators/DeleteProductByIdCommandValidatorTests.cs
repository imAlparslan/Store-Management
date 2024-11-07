using CatalogManagement.Application.Products;
using CatalogManagement.Application.Products.Commands.DeleteProductById;
using FluentAssertions;
using FluentAssertions.Execution;

namespace CatalogManagement.Application.Tests.Validations.ProductValidators;
public class DeleteProductByIdCommandValidatorTests
{

    [Fact]
    public void Validator_RetursSuccess_WhenIdValid()
    {
        var command = new DeleteProductByIdCommand(Guid.NewGuid());
        var validator = new DeleteProductByIdCommandValidator();

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }
    }

    [Theory]
    [MemberData(nameof(InvalidGuidData))]
    public void Validator_ReturnsValidationError_WhenProductIdInvalid(Guid id)
    {
        var command = new DeleteProductByIdCommand(id);
        var validator = new DeleteProductByIdCommandValidator();

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Select(x => x.PropertyName)
                .Should()
                .ContainSingle(nameof(command.Id));
        }
    }
    public static IEnumerable<object[]> InvalidGuidData => new List<object[]> {
        new object[] { null },
        new object[] { Guid.Empty },
        new object[] { default(Guid) }
    };
}
