namespace InventoryManagement.Api;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class StockEndpoints
    {
        private const string StockBase = $"{ApiBase}/stocks";
        // TODO: stockbase/{stockId}/add-item
        public const string AddItem = $"{StockBase}/add-item";
        public const string GetStockById = $"{StockBase}/{{id:guid}}";
        public const string GetAllStocksByGroupId = $"{StockBase}/get-by-group/";
    }

}
