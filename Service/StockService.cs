using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;

namespace ShoppingCart.Service;

public class StockService
{
    private readonly AppDbContext _dbContext;

    public StockService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Model.Stock>> GetStocksAsync()
    {
        return await _dbContext.Stocks
            .AsNoTracking()
            .OrderBy(stock => stock.id)
            .ToListAsync();
    }

    public async Task<bool> UpdateStockAsync(int productId, int orderCount)
    {
        if (productId <= 0 || orderCount <= 0)
        {
            return false;
        }

        var affectedRows = await _dbContext.Stocks
            .Where(stock => stock.productId == productId && stock.total >= orderCount)
            .ExecuteUpdateAsync(update => update
                .SetProperty(stock => stock.total, stock => stock.total - orderCount));

        return affectedRows > 0;
    }

    public async Task<bool> UpdateStocksAsync(IReadOnlyCollection<Model.UpdateStockRequest> requests)
    {
        if (requests.Count == 0 || requests.Any(request => request.productId <= 0 || request.orderCount <= 0))
        {
            return false;
        }

        var groupedRequests = requests
            .GroupBy(request => request.productId)
            .Select(group => new
            {
                ProductId = group.Key,
                OrderCount = group.Sum(request => request.orderCount)
            })
            .ToList();

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        foreach (var request in groupedRequests)
        {
            var affectedRows = await _dbContext.Stocks
                .Where(stock => stock.productId == request.ProductId && stock.total >= request.OrderCount)
                .ExecuteUpdateAsync(update => update
                    .SetProperty(stock => stock.total, stock => stock.total - request.OrderCount));

            if (affectedRows == 0)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        await transaction.CommitAsync();
        return true;
    }
}
