using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate.Errors;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.ProductGroups;
internal class DeleteProductGroupByIdCommandHandler(IProductGroupRepository productGroupRepository)
        : IRequestHandler<DeleteProductGroupByIdCommand, Result<bool>>
{
    private readonly IProductGroupRepository productGroupRepository = productGroupRepository;

    public async Task<Result<bool>> Handle(DeleteProductGroupByIdCommand request, CancellationToken cancellationToken)
    {
        var deletedResult = await productGroupRepository.DeleteByIdAsync(request.Id, cancellationToken);
        if (deletedResult)
        {
            return Result<bool>.Success(deletedResult);
        }
        return Result<bool>.Fail(ProductGroupError.NotDeleted);
    }
}
