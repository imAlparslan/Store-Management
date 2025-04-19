using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.Errors;

namespace CatalogManagement.Application.ProductGroups;
internal sealed class GetProductGroupByIdQueryHandler(IProductGroupRepository productGroupRepository)
        : IQueryHandler<GetProductGroupByIdQuery, Result<ProductGroup>>
{
    private readonly IProductGroupRepository productGroupRepository = productGroupRepository;

    public async Task<Result<ProductGroup>> Handle(GetProductGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var productGroup = await productGroupRepository.GetByIdAsync(request.Id, cancellationToken);
        if (productGroup is null)
        {
            return ProductGroupError.NotFoundById;
        }
        return productGroup;
    }
}
