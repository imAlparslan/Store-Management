using FluentAssertions;
using StoreDefinition.Application.Groups.Commands.CreateGroup;
using StoreDefinition.Application.Tests.Common.Factories.GroupFactories;

namespace StoreDefinition.Application.Tests.Groups.Validations;
public class CreateGroupCommandValidatorTests
{
    private readonly CreateGroupCommandValidator validator;
    public CreateGroupCommandValidatorTests()
    {
        validator = new CreateGroupCommandValidator();
    }

    [Fact]
    public void Validator_ReturnsTrue_WhenCommandValid()
    {
        var command = GroupCommandFactory.GroupCreateCommand();

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void Validator_ReturnsFalse_WhenNameInvalid(string invalid)
    {
        var command = GroupCommandFactory.GroupCreateCommand(name: invalid);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain(nameof(command.Name));
    }


    [Theory]
    [MemberData(nameof(invalidStrings))]
    public void Validator_ReturnsFalse_WhenDescriptionInvalid(string invalid)
    {
        var command = GroupCommandFactory.GroupCreateCommand(description: invalid);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain(nameof(command.Description));
    }

    [Fact]
    public void Validator_ReturnsFalse_WhenCommandInvalid()
    {
        var command = GroupCommandFactory.GroupCreateCommand(name: "", description: "");

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(2);
        result.Errors.Select(x => x.PropertyName)
            .Should().Contain([nameof(command.Description), nameof(command.Name)]);
    }

    public static readonly TheoryData<string> invalidStrings = ["", " ", null];

}
