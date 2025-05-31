using StoreDefinition.Application.Groups.Commands.DeleteGroup;

namespace StoreDefinition.Application.Tests.Groups.Validations;

public class DeleteGroupCommandValidatorTests
{
    private readonly DeleteGroupCommandValidator validator;
    public DeleteGroupCommandValidatorTests()
    {
        validator = new DeleteGroupCommandValidator();
    }

    [Fact]
    public void Validator_ReturnsTrue_WhenCommandValid()
    {
        var command = new DeleteGroupCommand(Guid.NewGuid());

        var result = validator.Validate(command);

        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validator_ReturnsFalse_WhenCommandInvalid()
    {
        var command = new DeleteGroupCommand(Guid.Empty);

        var result = validator.Validate(command);

        result.IsValid.ShouldBeFalse();
        result.Errors.Select(x => x.PropertyName)
            .ShouldContain(nameof(command.GroupId));
    }
}
