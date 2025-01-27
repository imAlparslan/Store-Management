using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductAggregate.ValueObjects;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.Products;
internal sealed class CreateProductCommandHandler(IProductRepository productRepository)
    : ICommandHandler<CreateProductCommand, Result<Product>>
{
    private readonly IProductRepository productRepository = productRepository;

    public async Task<Result<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        ProductName productName = new(request.ProductName);
        ProductCode productCode = new(request.ProductCode);
        ProductDefinition productDefinition = new(request.ProductDefinition);

        Product product = Product.Create(productName, productCode, productDefinition, request.GroupIds);

        return await productRepository.InsertAsync(product, cancellationToken);

    }
}
