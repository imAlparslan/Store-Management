using CatalogManagement.Domain.Common.Exceptions;
using CatalogManagement.SharedKernel;

namespace CatalogManagement.Domain.ProductAggregate.Exceptions;
public class ProductException : DomainException
{
    private ProductException(string code, string? description) : base(code, description)
    {

    }
    public static DomainException Create(Error error)
    {
        return new ProductException(error.Code, error.Description);
    }

}
