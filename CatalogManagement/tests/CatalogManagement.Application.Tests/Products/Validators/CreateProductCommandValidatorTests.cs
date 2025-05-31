using CatalogManagement.Application.Products.Commands.CreateProduct;

namespace CatalogManagement.Application.Tests.Products.Validators;

public class CreateProductCommandValidatorTests
{
    private readonly CreateProductCommandValidator validator;
    public CreateProductCommandValidatorTests()
    {
        validator = new();
    }

    [Fact]
    public void Validator_ReturnsValidResult_WhenDataValid()
    {
        var command = CreateProductCommandFactory.CreateValid();

        var result = validator.Validate(command);

        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Theory]
    [InlineData("", "", "")]
    [InlineData(" ", " ", " ")]
    [InlineData(null, null, null)]
    public void Validator_ReturnsInvalidResult_WhenDataInvalid(string productName, string productCode, string productDefinition)
    {
        var command = CreateProductCommandFactory.CreateCustom(productName, productCode, productDefinition);

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(3);
        result.Errors.Select(x => x.PropertyName)
            .ShouldBeSubsetOf([nameof(command.ProductName),
                            nameof(command.ProductCode),
                            nameof(command.ProductDefinition)]);
    }


    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Validator_ReturnsInvalidResult_WhenProductNameInvalid(string productName)
    {
        var command = CreateProductCommandFactory.CreateWithName(productName);

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(command.ProductName));
    }
    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Validator_ReturnsInvalidResult_WhenProductCodeInvalid(string productCode)
    {
        var command = CreateProductCommandFactory.CreateWithCode(productCode);

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(command.ProductCode));
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Validator_ReturnsInvalidResult_WhenProductDefinitionInvalid(string productDefinition)
    {
        var command = CreateProductCommandFactory.CreateWithDefinition(productDefinition);

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(command.ProductDefinition));
    }
}
