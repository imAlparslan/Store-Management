using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products;
internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<Product>>
{
    private readonly IProductRepository productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<Result<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return Result<Product>.Fail(ProductError.NotFoundById);
        }

        product.ChangeName(new(request.ProductName));
        product.ChangeCode(new(request.ProductCode));
        product.ChangeDefinition(new(request.ProductDefinition));

        var result = await productRepository.UpdateAsync(product, cancellationToken);

        return Result<Product>.Success(result);

    }
}
