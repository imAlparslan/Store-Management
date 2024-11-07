using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductAggregate.ValueObjects;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products;
internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Product>>
{
    private readonly IProductRepository productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<Result<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        ProductName productName = new(request.ProductName);
        ProductCode productCode = new(request.ProductCode);
        ProductDefinition productDefinition = new(request.ProductDefinition);

        Product product = new(productName, productCode, productDefinition);

        await productRepository.InsertAsync(product, cancellationToken);

        return Result<Product>.Success(product);
    }
}
