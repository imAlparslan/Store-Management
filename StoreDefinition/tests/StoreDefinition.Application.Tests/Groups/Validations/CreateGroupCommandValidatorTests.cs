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

        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public void Validator_ReturnsFalse_WhenNameInvalid(string invalid)
    {
        var command = GroupCommandFactory.GroupCreateCommand(name: invalid);

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(command.Name));
    }


    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public void Validator_ReturnsFalse_WhenDescriptionInvalid(string invalid)
    {
        var command = GroupCommandFactory.GroupCreateCommand(description: invalid);

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(command.Description));
    }

    [Fact]
    public void Validator_ReturnsFalse_WhenCommandInvalid()
    {
        var command = GroupCommandFactory.GroupCreateCommand(name: "", description: "");

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(2);
        result.Errors.Select(x => x.PropertyName)
            .ShouldBeSubsetOf([nameof(command.Description), nameof(command.Name)]);
    }
}
