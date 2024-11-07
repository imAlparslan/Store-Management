using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products;
internal class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, Result<bool>>
{
    private readonly IProductRepository productRepository;

    public DeleteProductByIdCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<Result<bool>> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await productRepository.DeleteByIdAsync(request.Id);
        if (result)
        {
            return Result<bool>.Success(result);
        }

        return Result<bool>.Fail(ProductError.NotDeleted);

    }
}
