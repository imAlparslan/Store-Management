using FluentValidation;

namespace StoreDefinition.Application.Groups.Commands.RemoveShopFromGroup;
internal sealed class RemoveShopFromGroupCommandValidator : AbstractValidator<RemoveShopFromGroupCommand>
{
    public RemoveShopFromGroupCommandValidator()
    {
        RuleFor(x => x.GroupId).NotEmpty();
        RuleFor(x => x.ShopId).NotEmpty();
    }
}