using FluentValidation;

namespace CatalogManagement.Application.ProductGroups.Commands.AddProduct;
internal class AddProductToGroupCommandValidator : AbstractValidator<AddProductToGroupCommand>
{

    public AddProductToGroupCommandValidator()
    {
        RuleFor(x => x.ProductGroupId)
            .NotEmpty();

        RuleFor(x => x.ProductId)
            .NotEmpty();
    }
}
