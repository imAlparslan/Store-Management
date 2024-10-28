using FluentValidation;

namespace CatalogManagement.Application.Products.Commands.DeleteProductById;
internal class DeleteProductCommandValidator : AbstractValidator<DeleteProductByIdCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
