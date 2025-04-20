using InventoryManagement.Application.Common;
using InventoryManagement.Infrastructure.Persistence;

namespace InventoryManagement.Infrastructure.Repositories;
public class StockRepository(InventoryDbContext dbContext)
    : IStockRepository
{
    private readonly InventoryDbContext _dbContext = dbContext;

}
