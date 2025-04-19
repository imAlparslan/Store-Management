using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductAggregate.Errors;

namespace CatalogManagement.Application.Products;
internal sealed class UpdateProductCommandHandler(IProductRepository productRepository)
    : ICommandHandler<UpdateProductCommand, Result<Product>>
{
    private readonly IProductRepository productRepository = productRepository;

    public async Task<Result<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return ProductError.NotFoundById;
        }

        product.ChangeName(new(request.ProductName));
        product.ChangeCode(new(request.ProductCode));
        product.ChangeDefinition(new(request.ProductDefinition));

        return await productRepository.UpdateAsync(product, cancellationToken);

    }
}
