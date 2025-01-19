using FluentValidation;

namespace StoreDefinition.Application.Shops.Commands.CreateShop;
internal class CreateShopCommandValidator : AbstractValidator<CreateShopCommand>
{
    public CreateShopCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.Street).NotEmpty();
    }
}
