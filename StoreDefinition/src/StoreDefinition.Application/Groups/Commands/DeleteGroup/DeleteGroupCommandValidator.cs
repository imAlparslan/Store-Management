using FluentValidation;

namespace StoreDefinition.Application.Groups.Commands.DeleteGroup;
internal sealed class DeleteGroupCommandValidator : AbstractValidator<DeleteGroupCommand>
{
    public DeleteGroupCommandValidator()
    {
        RuleFor(x => x.GroupId).NotEmpty();
    }
}
