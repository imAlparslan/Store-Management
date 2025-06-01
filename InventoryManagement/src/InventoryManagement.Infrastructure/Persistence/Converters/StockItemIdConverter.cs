using InventoryManagement.Domain.StockAggregateRoot.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InventoryManagement.Infrastructure.Persistence.Converters;
public class StockItemIdConverter() : ValueConverter<StockItemId, Guid>(
    stockItem => stockItem.Value, guid => guid);

