using CatalogManagement.SharedKernel;

namespace CatalogManagement.Domain.ProductAggregate.Errors;
public static class ProductError
{
    static ProductError()
    {
    }
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

    public static readonly Error ProductGroupNotDeletedFromProduct = new Error(
       "Invalid.Product.ProductGroupNotDeleted",
       ErrorType.NotUpdated,
       "Product does not have the group.");

    public static readonly Error ProductGroupNotAddedToProduct = new Error(
       "Invalid.Product.ProductGroupNotAdded",
       ErrorType.NotUpdated,
       "Product group does not added to product.");

}
