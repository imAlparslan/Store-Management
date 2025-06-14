﻿using InventoryManagement.Domain.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InventoryManagement.Infrastructure.Persistence.Converters;
internal class StoreIdConverter : ValueConverter<StoreId, Guid>
{
    public StoreIdConverter() : base(
        storeId => storeId.Value,
        guid => new StoreId(guid))
    {

    }
}
