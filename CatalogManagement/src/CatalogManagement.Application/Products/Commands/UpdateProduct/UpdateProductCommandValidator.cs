using FluentValidation;

namespace CatalogManagement.Application.Products.Commands.UpdateProduct;
internal class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.ProductName)
            .NotEmpty();

        RuleFor(x => x.ProductCode)
            .NotEmpty();

        RuleFor(x => x.ProductDefinition)
            .NotEmpty();
    }
}
