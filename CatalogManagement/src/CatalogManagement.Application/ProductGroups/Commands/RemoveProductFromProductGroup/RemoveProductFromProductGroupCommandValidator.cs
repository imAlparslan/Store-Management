using FluentValidation;

namespace CatalogManagement.Application.ProductGroups.Commands.RemoveProductFromProductGroup;
internal class RemoveProductFromProductGroupCommandValidator : AbstractValidator<RemoveProductFromProductGroupCommand>
{
    public RemoveProductFromProductGroupCommandValidator()
    {
        RuleFor(x => x.ProductGroupId)
            .NotEmpty();

        RuleFor(x => x.ProductId)
            .NotEmpty();
    }
}
