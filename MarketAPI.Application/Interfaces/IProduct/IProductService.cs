using MarketAPI.Application.Dtos.ProductDto;
using MarketAPI.Domain.Entities;

namespace MarketAPI.Application.Interfaces.IProduct;

public interface IProductService
{
    Task <Product> GetProductAsync(Guid id);
    Task<List<Product>> GetProductsAsync();
    Task AddProductAsync(AddProductDto dto);
    Task UpdateProductAsync(Guid id, UpdateProductDto dto);
    Task DeleteProductAsync(Guid id);
}