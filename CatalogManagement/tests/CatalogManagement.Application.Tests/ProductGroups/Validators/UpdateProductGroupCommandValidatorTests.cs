using CatalogManagement.Application.ProductGroups.Commands.UpdateProductGroup;

namespace CatalogManagement.Application.Tests.ProductGroups.Validators;

public class UpdateProductGroupCommandValidatorTests
{
    private readonly UpdateProductGroupCommandValidator validator;
    public UpdateProductGroupCommandValidatorTests()
    {
        validator = new();
    }

    [Fact]
    public void Validator_ReturnsValidResult_WhenDataValid()
    {
        var command = UpdateProductGroupCommandFactory.CreateValid();

        var result = validator.Validate(command);

        result.ShouldSatisfyAllConditions(
            x => result.IsValid.ShouldBeTrue(),
            x => result.Errors.ShouldBeEmpty()
        );
    }

    [Theory]
    [InlineData(default, "", "")]
    [InlineData(default, " ", " ")]
    [InlineData(null, null, null)]
    public void Validator_ReturnsValidationError_WhenDataInvalid(Guid id, string group, string productGroupDescription)
    {
        var command = UpdateProductGroupCommandFactory.CreateCustom(id, group, productGroupDescription);

        var result = validator.Validate(command);

        result.ShouldSatisfyAllConditions(
            x => result.IsValid.ShouldBeFalse(),
            x => result.Errors.Count.ShouldBe(3),
            x => result.Errors.Select(x => x.PropertyName)
                .ShouldBeSubsetOf([nameof(command.Id), nameof(command.Name), nameof(command.Description)])
        );
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Validator_ReturnsValidationError_WhenProductGroupNameInvalid(string productGroupName)
    {
        var command = UpdateProductGroupCommandFactory.CreateWithName(productGroupName);

        var result = validator.Validate(command);

        result.ShouldSatisfyAllConditions(
            x => result.IsValid.ShouldBeFalse(),
            x => result.Errors.Count.ShouldBe(1),
            x => result.Errors.Select(x => x.PropertyName)
                .ShouldContain(nameof(command.Name))
        );
    }

    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public void Validator_ReturnsValidationError_WhenProductIdInvalid(Guid productGroupId)
    {
        var command = UpdateProductGroupCommandFactory.CreateWithId(productGroupId);

        var result = validator.Validate(command);

        result.ShouldSatisfyAllConditions(
            x => result.IsValid.ShouldBeFalse(),
            x => result.Errors.Count.ShouldBe(1),
            x => result.Errors.Select(x => x.PropertyName)
                .ShouldContain(nameof(command.Id))
        );
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Validator_ReturnsValidationError_WhenProductGroupDescriptionInvalid(string productGroupDescription)
    {
        var command = UpdateProductGroupCommandFactory.CreateWithDefinition(productGroupDescription);

        var result = validator.Validate(command);

        result.ShouldSatisfyAllConditions(
            x => result.IsValid.ShouldBeFalse(),
            x => result.Errors.Count.ShouldBe(1),
            x => result.Errors.Select(x => x.PropertyName)
                .ShouldContain(nameof(command.Description))
        );
    }
}
