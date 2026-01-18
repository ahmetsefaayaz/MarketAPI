using MarketAPI.Domain.Entities;

namespace MarketAPI.Application.Interfaces.IProduct;

public interface IProductRepository
{
    Task <Product?> GetProductAsync(Guid productId);
    Task<List<Product>> GetProductsAsync();
    Task AddProductAsync(Product product);
    Task DeleteProductAsync(Guid productId);
    Task UpdateProductAsync(Product product);
    
}