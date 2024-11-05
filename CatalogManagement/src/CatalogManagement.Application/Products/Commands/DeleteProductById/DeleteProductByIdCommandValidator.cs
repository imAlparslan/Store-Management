using FluentValidation;

namespace CatalogManagement.Application.Products.Commands.DeleteProductById;
internal class DeleteProductByIdCommandValidator : AbstractValidator<DeleteProductByIdCommand>
{
    public DeleteProductByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
