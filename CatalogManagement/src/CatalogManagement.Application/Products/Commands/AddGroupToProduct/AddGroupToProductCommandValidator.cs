using FluentValidation;

namespace CatalogManagement.Application.Products.Commands.AddGroup;
internal class AddGroupToProductCommandValidator : AbstractValidator<AddGroupToProductCommand>
{
    public AddGroupToProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.GroupId)
            .NotEmpty();
    }
}
