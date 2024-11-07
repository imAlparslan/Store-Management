using FluentValidation;

namespace CatalogManagement.Application.ProductGroups.Commands.UpdateProductGroup;
internal class UpdateProductGroupCommandValidator : AbstractValidator<UpdateProductGroupCommand>
{
    public UpdateProductGroupCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}
