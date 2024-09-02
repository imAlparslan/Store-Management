using CatalogManagement.Domain.Common.Errors;
using CatalogManagement.Domain.Common.Exceptions;

namespace CatalogManagement.Domain.ProductGroupAggregate.Exceptions;
public class ProductGroupException : DomainException
{
    private ProductGroupException(string code, string? message) : base(code, message)
    {
    }

    public static ProductGroupException Create(Error error)
    {
        return new ProductGroupException(error.Code, error.Description);
    }
}