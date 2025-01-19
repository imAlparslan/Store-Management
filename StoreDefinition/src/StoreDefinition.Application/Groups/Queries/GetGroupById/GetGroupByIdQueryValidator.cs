using FluentValidation;

namespace StoreDefinition.Application.Groups.Queries.GetGroupById;
internal sealed class GetGroupByIdQueryValidator : AbstractValidator<GetGroupByIdQuery>
{
    public GetGroupByIdQueryValidator()
    {
        RuleFor(x => x.GroupId).NotEmpty();
    }
}
