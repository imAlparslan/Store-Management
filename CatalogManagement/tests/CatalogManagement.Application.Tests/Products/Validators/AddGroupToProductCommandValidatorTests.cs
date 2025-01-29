using CatalogManagement.Application.Products.Commands.AddGroup;

namespace CatalogManagement.Application.Tests.Products.Validators;

public class AddGroupToProductCommandValidatorTests
{
    private readonly AddGroupToProductCommandValidator validator;

    public AddGroupToProductCommandValidatorTests()
    {
        validator = new AddGroupToProductCommandValidator();
    }

    [Fact]
    public void Validator_ReturnsValidResult_WhenDataValid()
    {
        var command = new AddGroupToProductCommand(Guid.NewGuid(), Guid.NewGuid());

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public void Validator_ReturnsValidationProblem_WhenProductGroupIdInvalid(Guid invalidProductGroupId)
    {
        var command = new AddGroupToProductCommand(Guid.NewGuid(), invalidProductGroupId);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain(nameof(command.GroupId));
    }

    [Theory]
    [ClassData(typeof(InvalidGuidData))]
    public void Validator_ReturnsValidationProblem_WhenProductIdInvalid(Guid invalidProductId)
    {
        var command = new AddGroupToProductCommand(invalidProductId, Guid.NewGuid());

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain(nameof(command.ProductId));
    }

    [Fact]
    public void Validator_ReturnsValidationProblem_WhenDataInvalid()
    {
        var command = new AddGroupToProductCommand(Guid.Empty, Guid.Empty);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(2);
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain([nameof(command.ProductId), nameof(command.GroupId)]);
    }

}
