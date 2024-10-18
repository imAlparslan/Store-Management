using CatalogManagement.Application.Common;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products.Queries.GetAllProducts;
internal class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<IEnumerable<Product>>>
{
    private readonly IProductRepository productRepository;

    public GetAllProductsQueryHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<Result<IEnumerable<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync(cancellationToken);

        return Result<IEnumerable<Product>>.Success(products);

    }
}
