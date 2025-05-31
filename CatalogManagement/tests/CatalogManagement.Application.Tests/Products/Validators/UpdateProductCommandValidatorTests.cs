using CatalogManagement.Application.Products.Commands.UpdateProduct;

namespace CatalogManagement.Application.Tests.Products.Validators;

public class UpdateProductCommandValidatorTests
{
    private readonly UpdateProductCommandValidator validator;
    public UpdateProductCommandValidatorTests()
    {
        validator = new();
    }

    [Fact]
    public void Validator_ReturnsValidResult_WhenDataValid()
    {
        var command = UpdateProductCommandFactory.CreateValid();

        var result = validator.Validate(command);

        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Theory]
    [InlineData(default, "", "", "")]
    [InlineData(default, " ", " ", " ")]
    [InlineData(null, null, null, null)]
    public void Validator_ReturnsValidationError_WhenDataInvalid(Guid id, string productName, string productCode, string productDefinition)
    {
        var command = UpdateProductCommandFactory.CreateCustom(id, productName, productCode, productDefinition);

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(4);
        result.Errors.Select(x => x.PropertyName)
            .ShouldBeSubsetOf([nameof(command.Id), nameof(command.ProductName), nameof(command.ProductCode), nameof(command.ProductDefinition)]);
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Validator_ReturnsValidationError_WhenProductNameInvalid(string productName)
    {
        var command = UpdateProductCommandFactory.CreateWithName(productName);

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.Select(x => x.PropertyName)
            .ShouldHaveSingleItem(nameof(command.ProductName));
    }

    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public void Validator_ReturnsValidationError_WhenProductIdInvalid(Guid productId)
    {
        var command = UpdateProductCommandFactory.CreateWithId(productId);

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.Select(x => x.PropertyName)
            .ShouldHaveSingleItem(nameof(command.Id));
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Validator_ReturnsValidationError_WhenProductCodeInvalid(string productCode)
    {
        var command = UpdateProductCommandFactory.CreateWithCode(productCode);

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.Select(x => x.PropertyName)
            .ShouldHaveSingleItem(nameof(command.ProductCode));
    }

    [Theory]
    [ClassData(typeof(InvalidStringData))]
    public void Validator_ReturnsValidationError_WhenProductDefinitionInvalid(string productDefinition)
    {
        var command = UpdateProductCommandFactory.CreateWithDefinition(productDefinition);

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.Select(x => x.PropertyName)
            .ShouldHaveSingleItem(nameof(command.ProductDefinition));
    }
}
