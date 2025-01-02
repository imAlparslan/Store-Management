using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.Products;
internal sealed class GetAllProductsQueryHandler(IProductRepository productRepository)
    : IQueryHandler<GetAllProductsQuery, Result<IEnumerable<Product>>>
{
    private readonly IProductRepository productRepository = productRepository;

    public async Task<Result<IEnumerable<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync(cancellationToken);

        return Result<IEnumerable<Product>>.Success(products);

    }
}
