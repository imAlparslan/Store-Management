using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate;

namespace CatalogManagement.Application.ProductGroups;
internal sealed class GetAllProductGroupsQueryHandler(IProductGroupRepository productGroupRepository)
        : IQueryHandler<GetAllProductGroupsQuery, Result<IEnumerable<ProductGroup>>>
{
    private readonly IProductGroupRepository productGroupRepository = productGroupRepository;

    public async Task<Result<IEnumerable<ProductGroup>>> Handle(GetAllProductGroupsQuery request, CancellationToken cancellationToken)
    {
        var productGroups = await productGroupRepository.GetAllAsync(cancellationToken);
        return Result<IEnumerable<ProductGroup>>.Success(productGroups);
    }
}
