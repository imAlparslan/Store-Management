using FluentAssertions;
using StoreDefinition.Application.Groups.Commands.UpdateGroup;
using StoreDefinition.Application.Tests.Common.Factories.GroupFactories;

namespace StoreDefinition.Application.Tests.Groups.Validations;
public class UpdateGroupCommandValidatorTests
{
    private readonly UpdateGroupCommandValidator validator;
    public UpdateGroupCommandValidatorTests()
    {
        validator = new UpdateGroupCommandValidator();
    }

    [Fact]
    public void Validator_ReturnsTrue_WhenDataValid()
    {
        var command = GroupCommandFactory.GroupUpdateCommand(Guid.NewGuid());

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void Validator_ReturnsFalse_WhenNameInvalid(string invalid)
    {
        var command = GroupCommandFactory.GroupUpdateCommand(id: Guid.NewGuid(), name: invalid);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain(nameof(command.Name));
    }
    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void Validator_ReturnsFalse_WhenDescription(string invalid)
    {
        var command = GroupCommandFactory.GroupUpdateCommand(id: Guid.NewGuid(), description: invalid);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain(nameof(command.Description));
    }

    [Fact]
    public void Validator_ReturnsFalse_WhenIdInvalid()
    {
        var command = GroupCommandFactory.GroupUpdateCommand(id: Guid.Empty);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain(nameof(command.GroupId));
    }

    [Fact]
    public void Validator_ReturnsFalse_WhenCommandInvalid()
    {
        var command = GroupCommandFactory.GroupUpdateCommand(id: Guid.Empty, name: " ", description: " ");

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(3);
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain([nameof(command.GroupId), nameof(command.Name), nameof(command.Description)]);
    }
    public static readonly TheoryData<string> invalidStrings = ["", " ", null];

}
