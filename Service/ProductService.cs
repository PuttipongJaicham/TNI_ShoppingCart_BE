using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;

namespace ShoppingCart.Service;

public class ProductService
{
    private readonly AppDbContext _dbContext;

    public ProductService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Model.Product>> GetProductsAsync()
    {
        return await _dbContext.Products
            .Select(product => new Model.Product
            {
                id = product.id,
                name = product.name,
                price = product.price,
                stockTotal = _dbContext.Stocks
                    .Where(stock => stock.productId == product.id)
                    .Sum(stock => (int?)stock.total) ?? 0
            })
            .OrderBy(product => product.id)
            .ToListAsync();
    }

    public async Task<Model.Product?> GetProductByIdAsync(int id)
    {
        return await _dbContext.Products
            .Where(product => product.id == id)
            .Select(product => new Model.Product
            {
                id = product.id,
                name = product.name,
                price = product.price,
                stockTotal = _dbContext.Stocks
                    .Where(stock => stock.productId == product.id)
                    .Sum(stock => (int?)stock.total) ?? 0
            })
            .FirstOrDefaultAsync();
    }
}
