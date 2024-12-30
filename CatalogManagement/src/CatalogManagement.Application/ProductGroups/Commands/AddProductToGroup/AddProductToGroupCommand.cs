using CatalogManagement.Domain.ProductGroupAggregate;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.ProductGroups.Commands.AddProduct;
public record AddProductToGroupCommand(Guid ProductGroupId, Guid ProductId) : IRequest<Result<ProductGroup>>;