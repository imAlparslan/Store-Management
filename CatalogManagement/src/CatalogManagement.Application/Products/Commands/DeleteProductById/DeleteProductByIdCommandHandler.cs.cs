﻿using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.Domain.ProductAggregate.Events;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products;
internal class DeleteProductByIdCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductByIdCommand, Result<bool>>
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
        var result = await productRepository.DeleteByIdAsync(request.Id, cancellationToken);

        if (!result)
        {
            return ProductError.NotFoundById;
        }
        return result;
    }
}
