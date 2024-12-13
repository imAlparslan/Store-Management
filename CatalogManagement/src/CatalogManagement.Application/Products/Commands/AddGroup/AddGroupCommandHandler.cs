using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products.Commands.AddGroup;
internal class AddGroupCommandHandler(IProductRepository productRepository) : IRequestHandler<AddGroupCommand, Result<Product>>
{
    private readonly IProductRepository productRepository = productRepository;

    public async Task<Result<Product>> Handle(AddGroupCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return ProductError.NotFoundById;
        }

        product.AddGroup(request.GroupId);

        await productRepository.UpdateAsync(product, cancellationToken);

        return product;
    }
}
