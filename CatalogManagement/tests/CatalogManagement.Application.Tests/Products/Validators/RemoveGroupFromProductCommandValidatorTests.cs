using CatalogManagement.Application.Products.Commands.RemoveGroupFromProduct;

namespace CatalogManagement.Application.Tests.Products.Validators;

public class RemoveGroupFromProductCommandValidatorTests
{
    private readonly RemoveGroupFromProductCommandValidator validator;
    public RemoveGroupFromProductCommandValidatorTests()
    {
        validator = new();
    }

    [Fact]
    public void Validator_ReturnsValidResult_WhenDataValid()
    {
        var command = new RemoveGroupFromProductCommand(Guid.NewGuid(), Guid.NewGuid());

        var result = validator.Validate(command);

        result.IsValid.ShouldBeTrue();
    }
    [Fact]
    public void Validator_ReturnsValidationProblem_WhenDataInvalid()
    {
        var command = new RemoveGroupFromProductCommand(Guid.Empty, Guid.Empty);

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(2);
        result.Errors.Select(x => x.PropertyName)
            .ShouldBeSubsetOf([nameof(command.ProductId), nameof(command.GroupId)]);
    }
    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public void Validator_ReturnsValidationProblem_WhenProductGroupIdInvalid(Guid invalidProductGroupId)
    {
        var command = new RemoveGroupFromProductCommand(invalidProductGroupId, Guid.NewGuid());

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.Select(x => x.PropertyName)
            .ShouldHaveSingleItem(nameof(command.GroupId));
    }
    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public void Validator_ReturnsValidationProblem_WhenProductIdInvalid(Guid invalidProductId)
    {
        var command = new RemoveGroupFromProductCommand(Guid.NewGuid(), invalidProductId);

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.Select(x => x.PropertyName)
            .ShouldHaveSingleItem(nameof(command.ProductId));
    }
}
