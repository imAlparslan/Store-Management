using FluentValidation;

namespace CatalogManagement.Application.ProductGroups.Queries.GetProductGroupById;
internal class GetProductGroupByIdQueryValidator : AbstractValidator<GetProductGroupByIdQuery>
{
    public GetProductGroupByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
