﻿using CatalogManagement.Application.Common;
using CatalogManagement.Domain.ProductAggregate;
using CatalogManagement.Domain.ProductAggregate.Errors;
using CatalogManagement.SharedKernel;
using MediatR;

namespace CatalogManagement.Application.Products.Queries.GetProductById;
internal class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<Product>>
{
    private readonly IProductRepository productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<Result<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return Result<Product>.Fail(ProductError.NotFoundById);
        }

        return Result<Product>.Success(product);
    }
}
