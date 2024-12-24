using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.Domain.ProductAggregate.Events;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products.Commands.AddGroup;
internal class AddGroupToProductCommandHandler(IProductRepository productRepository) : IRequestHandler<AddGroupToProductCommand, Result<Product>>
{
    private readonly IProductRepository productRepository = productRepository;

    public async Task<Result<Product>> Handle(AddGroupToProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return ProductError.NotFoundById;
        }

        var isSuccess = product.AddGroup(request.GroupId);

        if (isSuccess)
        {
            product.AddDomainEvent(new NewGroupAddedToProductDomainEvent(request.GroupId, request.ProductId));
            await productRepository.UpdateAsync(product, cancellationToken);
        }


        return product;
    }
}
