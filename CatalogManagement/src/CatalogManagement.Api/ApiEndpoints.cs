namespace CatalogManagement.Api;
public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class ProductEndpoints
    {
        private const string ProductBase = $"{ApiBase}/products";

        public const string Create = ProductBase;
        public const string Update = $"{ProductBase}/{{id:guid}}";
        public const string Delete = $"{ProductBase}/{{id:guid}}";
        public const string GetById = $"{ProductBase}/{{id:guid}}";
        public const string GetAll = ProductBase;
        public const string AddGroupToProduct = $"{ProductBase}/{{productId:guid}}/add-group";
        public const string RemoveFromProductGroup = $"{ProductBase}/{{productId:guid}}/remove-group";
    }
    public static class ProductGroupEndpoints
    {
        private const string ProductGroupBase = $"{ApiBase}/product-groups";

        public const string Create = ProductGroupBase;
        public const string Update = $"{ProductGroupBase}/{{id:guid}}";
        public const string Delete = $"{ProductGroupBase}/{{id:guid}}";
        public const string GetById = $"{ProductGroupBase}/{{id:guid}}";
        public const string GetAll = ProductGroupBase;
        public const string AddProduct = $"{ProductGroupBase}/{{productGroupId:guid}}/add-product";

    }

}
