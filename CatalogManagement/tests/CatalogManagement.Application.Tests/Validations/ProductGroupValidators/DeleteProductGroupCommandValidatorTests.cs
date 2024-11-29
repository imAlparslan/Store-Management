using CatalogManagement.Application.ProductGroups;
using CatalogManagement.Application.ProductGroups.Commands.DeleteProductGroup;

namespace CatalogManagement.Application.Tests.Validations.ProductGroupValidators;
public class DeleteProductGroupCommandValidatorTests
{
    [Fact]
    public void Validator_RetursSuccess_WhenIdValid()
    {
        var command = new DeleteProductGroupByIdCommand(Guid.NewGuid());
        var validator = new DeleteProductGroupByIdCommandValidator();

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
        var command = new DeleteProductGroupByIdCommand(id);
        var validator = new DeleteProductGroupByIdCommandValidator();

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
