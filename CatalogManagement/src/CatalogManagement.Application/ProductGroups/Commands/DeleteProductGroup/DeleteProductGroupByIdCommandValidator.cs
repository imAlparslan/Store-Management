using FluentValidation;

namespace CatalogManagement.Application.ProductGroups.Commands.DeleteProductGroup;
internal class DeleteProductGroupByIdCommandValidator : AbstractValidator<DeleteProductGroupByIdCommand>
{
    public DeleteProductGroupByIdCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
