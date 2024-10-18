namespace CatalogManagement.Api;

public class ApiEndpoints
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
    }

}
