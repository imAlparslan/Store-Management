using CatalogManagement.Application.ProductGroups.Commands.UpdateProductGroup;

namespace CatalogManagement.Application.Tests.ProductGroups.Validators;
public class UpdateProductGroupCommandValidatorTests
{
    [Fact]
    public void Validator_ReturnsValidResult_WhenDataValid()
    {
        var command = UpdateProductGroupCommandFactory.CreateValid();
        var validator = new UpdateProductGroupCommandValidator();

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {

            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }
    }

    [Theory]
    [InlineData(default, "", "")]
    [InlineData(default, " ", " ")]
    [InlineData(null, null, null)]
    public void Validator_ReturnsValidationError_WhenDataInvalid(Guid id, string group, string productGroupDescription)
    {
        var command = UpdateProductGroupCommandFactory.CreateCustom(id, group, productGroupDescription);
        var validator = new UpdateProductGroupCommandValidator();

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(3);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(command.Id), nameof(command.Name), nameof(command.Description)]);
        }
    }



    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Validator_ReturnsValidationError_WhenProductGroupNameInvalid(string productGroupName)
    {
        var command = UpdateProductGroupCommandFactory.CreateWithName(productGroupName);
        var validator = new UpdateProductGroupCommandValidator();

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(command.Name)]);
        }
    }


    [Theory]
    [MemberData(nameof(InvalidGuidData))]
    public void Validator_ReturnsValidationError_WhenProductIdInvalid(Guid productGroupId)
    {
        var command = UpdateProductGroupCommandFactory.CreateWithId(productGroupId);
        var validator = new UpdateProductGroupCommandValidator();

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(command.Id)]);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Validator_ReturnsValidationError_WhenProductGroupDescriptionInvalid(string productGroupDescription)
    {
        var command = UpdateProductGroupCommandFactory.CreateWithDefinition(productGroupDescription);
        var validator = new UpdateProductGroupCommandValidator();

        var result = validator.Validate(command);

        using (AssertionScope scope = new())
        {
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.Select(x => x.PropertyName).Should()
                .Contain([nameof(command.Description)]);
        }
    }
    public static IEnumerable<object[]> InvalidGuidData => new List<object[]> {
        new object[] { null! },
        new object[] { Guid.Empty },
        new object[] { default(Guid) }
    };
}
