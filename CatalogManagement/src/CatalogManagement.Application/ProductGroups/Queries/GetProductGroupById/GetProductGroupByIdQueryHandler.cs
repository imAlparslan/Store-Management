using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.Errors;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.ProductGroups;
internal class GetProductGroupByIdQueryHandler(IProductGroupRepository productGroupRepository)
        : IRequestHandler<GetProductGroupByIdQuery, Result<ProductGroup>>
{
    private readonly IProductGroupRepository productGroupRepository = productGroupRepository;

    public async Task<Result<ProductGroup>> Handle(GetProductGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var productGroup = await productGroupRepository.GetByIdAsync(request.Id, cancellationToken);
        if (productGroup is null)
        {
            return Result<ProductGroup>.Fail(ProductGroupError.NotFoundById);
        }
        return Result<ProductGroup>.Success(productGroup);
    }
}
