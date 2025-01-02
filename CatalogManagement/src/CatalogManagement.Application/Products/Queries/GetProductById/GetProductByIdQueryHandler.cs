using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.Products;
internal sealed class GetProductByIdQueryHandler(IProductRepository productRepository)
    : IQueryHandler<GetProductByIdQuery, Result<Product>>
{
    private readonly IProductRepository productRepository = productRepository;

    public async Task<Result<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return ProductError.NotFoundById;
        }

        return product;
    }
}
