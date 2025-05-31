using CatalogManagement.Application.ProductGroups.Commands.CreateProductGroup;

namespace CatalogManagement.Application.Tests.ProductGroups.Validators;

public class CreateProductGroupCommandValidatorTests
{
    private readonly CreateProductGroupCommandValidator validator;
    public CreateProductGroupCommandValidatorTests()
    {
        validator = new();
    }
    [Fact]
    public void Validator_ReturnsValidResult_WhenDataValid()
    {
        var command = CreateProductGroupCommandFactory.CreateValid();

        var result = validator.Validate(command);

        result.ShouldSatisfyAllConditions(
            x => result.IsValid.ShouldBeTrue(),
            x => result.Errors.ShouldBeEmpty()
        );
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    [InlineData(null, null)]
    public void Validator_ReturnsInvalidResult_WhenDataInvalid(string productName, string productGroupDescription)
    {
        var command = CreateProductGroupCommandFactory.CreateCustom(productName, productGroupDescription);

        var result = validator.Validate(command);

        result.ShouldSatisfyAllConditions(
            x => result.IsValid.ShouldBeFalse(),
            x => result.Errors.Count.ShouldBe(2),
            x => result.Errors.Select(x => x.PropertyName)
                .ShouldBeSubsetOf([nameof(command.Name), nameof(command.Description)])
        );
    }


    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Validator_ReturnsInvalidResult_WhenProductNameInvalid(string productGroup)
    {
        var command = CreateProductGroupCommandFactory.CreateWithName(productGroup);

        var result = validator.Validate(command);

        result.ShouldSatisfyAllConditions(
            x => result.IsValid.ShouldBeFalse(),
            x => result.Errors.Select(x => x.PropertyName)
                .ShouldHaveSingleItem(nameof(command.Name))
        );
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Validator_ReturnsInvalidResult_WhenProductGroupDescriptionInvalid(string productGroupDescription)
    {
        var command = CreateProductGroupCommandFactory.CreateWithDefinition(productGroupDescription);

        var result = validator.Validate(command);

        result.ShouldSatisfyAllConditions(
            x => result.IsValid.ShouldBeFalse(),
            x => result.Errors.Select(x => x.PropertyName)
                .ShouldHaveSingleItem(nameof(command.Description))
        );
    }
}
