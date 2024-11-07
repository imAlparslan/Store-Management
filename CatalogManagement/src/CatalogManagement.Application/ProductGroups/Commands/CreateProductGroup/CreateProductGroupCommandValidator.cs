using FluentValidation;

namespace CatalogManagement.Application.ProductGroups.Commands.CreateProductGroup;
internal class CreateProductGroupCommandValidator : AbstractValidator<CreateProductGroupCommand>
{
    public CreateProductGroupCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}
