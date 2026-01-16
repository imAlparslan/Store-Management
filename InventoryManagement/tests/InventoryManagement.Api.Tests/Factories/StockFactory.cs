using System;

namespace InventoryManagement.Api.Tests.Factories;

public static class StockFactory
{

    public static Stock CreateValid() => new Stock(Guid.NewGuid(), new(), new());

}