using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.Domain.ProductGroupAggregate.ValueObjects;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.ProductGroups;
internal class CreateProductGroupCommandHandler(IProductGroupRepository productGroupRepository)
        : IRequestHandler<CreateProductGroupCommand, Result<ProductGroup>>
{
    private readonly IProductGroupRepository productGroupRepository = productGroupRepository;

    public async Task<Result<ProductGroup>> Handle(CreateProductGroupCommand request, CancellationToken cancellationToken)
    {
        ProductGroupName name = new(request.Name);
        ProductGroupDescription description = new(request.Description);
        ProductGroup productGroup = new(name, description);
        await productGroupRepository.InsertAsync(productGroup, cancellationToken);

        return Result<ProductGroup>.Success(productGroup);

    }
}
