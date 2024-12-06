using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.ProductGroups;
internal class GetAllProductGroupsQueryHandler(IProductGroupRepository productGroupRepository)
        : IRequestHandler<GetAllProductGroupsQuery, Result<IEnumerable<ProductGroup>>>
{
    private readonly IProductGroupRepository productGroupRepository = productGroupRepository;

    public async Task<Result<IEnumerable<ProductGroup>>> Handle(GetAllProductGroupsQuery request, CancellationToken cancellationToken)
    {
        var productGroups = await productGroupRepository.GetAllAsync(cancellationToken);
        return Result<IEnumerable<ProductGroup>>.Success(productGroups);
    }
}
