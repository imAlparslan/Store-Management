﻿using InventoryManagement.Domain.ItemAggregateRoot.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InventoryManagement.Infrastructure.Persistence.Converters;
internal class ItemIdConverter : ValueConverter<ItemId, Guid>
{
    public ItemIdConverter() : base(
        itemId => itemId.Value,
        guid => guid)

    {
    }
}

