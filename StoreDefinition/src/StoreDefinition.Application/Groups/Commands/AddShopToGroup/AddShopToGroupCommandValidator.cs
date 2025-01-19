using FluentValidation;

namespace StoreDefinition.Application.Groups.Commands.AddShopToGroup;
internal sealed class AddShopToGroupCommandValidator : AbstractValidator<AddShopToGroupCommand>
{
    public AddShopToGroupCommandValidator()
    {
        RuleFor(x => x.GroupId).NotEmpty();
        RuleFor(x => x.ShopId).NotEmpty();
    }
}
