using FluentValidation;

namespace CatalogManagement.Application.Products.Commands.CreateProduct;
internal class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty();

        RuleFor(x => x.ProductCode)
            .NotEmpty();

        RuleFor(x => x.ProductDefinition)
            .NotEmpty();

    }
}
