using FluentValidation;

namespace CatalogManagement.Application.Products.Commands.RemoveGroupFromProduct;
internal class RemoveGroupFromProductCommandValidator : AbstractValidator<RemoveGroupFromProductCommand>
{
    public RemoveGroupFromProductCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.GroupId).NotEmpty();
    }
}
