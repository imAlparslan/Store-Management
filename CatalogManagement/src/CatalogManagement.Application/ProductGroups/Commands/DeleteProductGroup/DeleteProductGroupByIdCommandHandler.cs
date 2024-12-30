using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate.Errors;
using CatalogManagement.Domain.ProductGroupAggregate.Events;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.ProductGroups;
internal sealed class DeleteProductGroupByIdCommandHandler(IProductGroupRepository productGroupRepository)
        : IRequestHandler<DeleteProductGroupByIdCommand, Result<bool>>
{
    private readonly IProductGroupRepository productGroupRepository = productGroupRepository;

    public async Task<Result<bool>> Handle(DeleteProductGroupByIdCommand request, CancellationToken cancellationToken)
    {
        var group = await productGroupRepository.GetByIdAsync(request.Id, cancellationToken);
        if (group is null)
        {
            return ProductGroupError.NotFoundById;
        }
        group.AddDomainEvent(new ProductGroupDeletedDomainEvent(group.Id));
        return await productGroupRepository.DeleteByIdAsync(request.Id, cancellationToken);
    }
}
