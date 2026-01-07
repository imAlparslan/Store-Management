namespace InventoryManagement.Api;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class StockEndpoints
    {
        public const string StockBase = $"{ApiBase}/stocks";
        public const string AddItem = $"{StockBase}/add-item";
        public const string GetStockById = $"{StockBase}/{{id:guid}}";
        public const string GetAllStocksByGroupId = $"{StockBase}/get-by-group/";
        public const string GetStocksByStoreId = $"{StockBase}/get-by-store/";
        public const string IncreaseStockCapacity = $"{StockBase}/{{id:guid}}/increase-stock-capacity";
    }

}
