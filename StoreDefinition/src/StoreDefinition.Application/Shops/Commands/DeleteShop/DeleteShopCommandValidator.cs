using FluentValidation;

namespace StoreDefinition.Application.Shops.Commands.DeleteShop;
internal sealed class DeleteShopCommandValidator : AbstractValidator<DeleteShopCommand>
{
    public DeleteShopCommandValidator()
    {
        RuleFor(x => x.ShopId).NotEmpty();
    }
}
