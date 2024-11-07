using FluentValidation;

namespace CatalogManagement.Application.Products.Queries.GetProductById;
internal class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
