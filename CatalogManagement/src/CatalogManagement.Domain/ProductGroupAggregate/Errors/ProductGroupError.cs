using CatalogManagement.SharedKernel;

namespace CatalogManagement.Domain.ProductGroupAggregate.Errors;
public static class ProductGroupError
{
    public static readonly Error InvalidName = new Error(
        "Invalid.ProductGroup.ProductGroupName",
        ErrorType.Validation,
        "Given product group 'name' is invalid.");

    public static readonly Error InvalidDescription = new Error(
        "Invalid.ProductGroup.ProductGroupDescription",
        ErrorType.Validation,
        "Given product group 'description' is invalid.");

    public static readonly Error NotFoundById = new Error(
       "Invalid.ProductGroup.NotFoundById",
       ErrorType.NotFound,
       "Product Group does not found.");

    public static readonly Error NotDeleted = new Error(
       "Invalid.ProductGroup.NotDeleted",
       ErrorType.NotDeleted,
       "Product Group does not deleted.");
}
