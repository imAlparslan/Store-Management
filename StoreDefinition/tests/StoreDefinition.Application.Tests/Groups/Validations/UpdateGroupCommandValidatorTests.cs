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

        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public void Validator_ReturnsFalse_WhenNameInvalid(string invalid)
    {
        var command = GroupCommandFactory.GroupUpdateCommand(id: Guid.NewGuid(), name: invalid);

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(command.Name));
    }
    [Theory]
    [ClassData(typeof(InvalidStrings))]
    public void Validator_ReturnsFalse_WhenDescription(string invalid)
    {
        var command = GroupCommandFactory.GroupUpdateCommand(id: Guid.NewGuid(), description: invalid);

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(command.Description));
    }

    [Fact]
    public void Validator_ReturnsFalse_WhenIdInvalid()
    {
        var command = GroupCommandFactory.GroupUpdateCommand(id: Guid.Empty);

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(command.GroupId));
    }

    [Fact]
    public void Validator_ReturnsFalse_WhenCommandInvalid()
    {
        var command = GroupCommandFactory.GroupUpdateCommand(id: Guid.Empty, name: " ", description: " ");

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(3);
        result.Errors.Select(x => x.PropertyName)
            .ShouldBeSubsetOf([nameof(command.GroupId), nameof(command.Name), nameof(command.Description)]);
    }

}
