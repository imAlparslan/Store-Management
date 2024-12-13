using FluentValidation;

namespace CatalogManagement.Application.Products.Commands.AddGroup;
internal class AddGroupCommandValidator : AbstractValidator<AddGroupCommand>
{
    public AddGroupCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.GroupId)
            .NotEmpty();
    }
}
