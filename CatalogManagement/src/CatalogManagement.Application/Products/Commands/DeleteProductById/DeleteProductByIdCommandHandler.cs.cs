using CatalogManagement.Application.Common.Interfaces;
using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.Domain.ProductAggregate.Events;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Application.Products;
internal sealed class DeleteProductByIdCommandHandler(IProductRepository productRepository)
    : ICommandHandler<DeleteProductByIdCommand, Result<bool>>
{
    private readonly IProductRepository productRepository = productRepository;

    public async Task<Result<bool>> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
        {
            return ProductError.NotFoundById;
        }
        product.AddDomainEvent(new ProductDeletedDomainEvent(product.Id));
        return await productRepository.DeleteByIdAsync(request.Id, cancellationToken);

    }
}
