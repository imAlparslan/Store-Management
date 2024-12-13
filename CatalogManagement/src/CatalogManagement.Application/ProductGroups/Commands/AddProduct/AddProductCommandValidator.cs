using FluentValidation;

namespace CatalogManagement.Application.ProductGroups.Commands.AddProduct;
internal class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{

    public AddProductCommandValidator()
    {
        RuleFor(x => x.ProductGroupId)
            .NotEmpty();

        RuleFor(x => x.ProductId)
            .NotEmpty();
    }
}
