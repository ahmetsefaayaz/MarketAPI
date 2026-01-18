using MarketAPI.Application.Interfaces.IProduct;
using MarketAPI.Domain.Entities;
using MarketAPI.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace MarketAPI.Persistence.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly MarketAPIDbContext _context;

    public ProductRepository(MarketAPIDbContext context)
    {
        _context = context;
    }
    
    public async Task<Product?> GetProductAsync(Guid productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        return product;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        var products = await _context.Products.ToListAsync();
        return products;
    }

    public async Task AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task DeleteProductAsync(Guid productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        _context.Products.Remove(product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
    }
}