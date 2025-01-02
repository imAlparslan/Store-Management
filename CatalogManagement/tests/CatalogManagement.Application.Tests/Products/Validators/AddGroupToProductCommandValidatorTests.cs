using CatalogManagement.Application.Products.Commands.AddGroup;

namespace CatalogManagement.Application.Tests.Products.Validators;
public class AddGroupToProductCommandValidatorTests
{

    [Fact]
    public async Task Validator_ReturnsValidResult_WhenDataValid()
    {
        var command = new AddGroupToProductCommand(Guid.NewGuid(), Guid.NewGuid());
        var validator = new AddGroupToProductCommandValidator();

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(InvalidGuidData))]
    public async Task Validator_ReturnsValidationProblem_WhenProductGroupIdInvalid(Guid invalidProductGroupId)
    {
        var command = new AddGroupToProductCommand(Guid.NewGuid(), invalidProductGroupId);
        var validator = new AddGroupToProductCommandValidator();

        var result = await validator.ValidateAsync(command, CancellationToken.None);

        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain(nameof(command.GroupId));
    }

    [Theory]
    [MemberData(nameof(InvalidGuidData))]
    public async Task Validator_ReturnsValidationProblem_WhenProductIdInvalid(Guid invalidProductId)
    {
        var command = new AddGroupToProductCommand(invalidProductId, Guid.NewGuid());
        var validator = new AddGroupToProductCommandValidator();

        var result = await validator.ValidateAsync(command, CancellationToken.None);

        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain(nameof(command.ProductId));
    }

    [Fact]
    public async Task Validator_ReturnsValidationProblem_WhenDataInvalid()
    {
        var command = new AddGroupToProductCommand(Guid.Empty, Guid.Empty);
        var validator = new AddGroupToProductCommandValidator();

        var result = await validator.ValidateAsync(command, CancellationToken.None);

        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(2);
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain([nameof(command.ProductId), nameof(command.GroupId)]);
    }

    public static IEnumerable<object[]> InvalidGuidData => new List<object[]> {
        new object[] { null! },
        new object[] { Guid.Empty },
        new object[] { default(Guid) }
    };
}
