using CatalogManagement.SharedKernel;

namespace CatalogManagement.Domain.ProductAggregate.Errors;
public class ProductError
{
    public static readonly Error InvalidName = new Error(
        "Invalid.Product.ProductName",
        ErrorType.Validation,
        "Given product 'name' is invalid.");

    public static readonly Error InvalidCode = new Error(
       "Invalid.Product.ProductCode",
       ErrorType.Validation,
       "Given product 'code' is invalid.");

    public static readonly Error InvalidDefinition = new Error(
       "Invalid.Product.ProductDefinition",
       ErrorType.Validation,
       "Given product 'definition' is invalid.");

    public static readonly Error NotFoundById = new Error(
       "Invalid.Product.NotFoundById",
       ErrorType.NotFound,
       "Product does not found.");

    public static readonly Error NotDeleted = new Error(
       "Invalid.Product.NotDeleted",
       ErrorType.NotDeleted,
       "Product does not deleted.");

}
