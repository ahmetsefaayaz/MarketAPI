using MarketAPI.Application.Dtos.ProductDto;
using MarketAPI.Application.Interfaces.IProduct;
using MarketAPI.Application.Interfaces.IUnitOfWork;
using MarketAPI.Domain.Entities;

namespace MarketAPI.Application.Services;

public class ProductService: IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Product> GetProductAsync(Guid id)
    {
        var product = await _unitOfWork.ProductRepository.GetProductAsync(id);
        if(product == null) throw new Exception("Product not found");
        return product;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        var products = await _unitOfWork.ProductRepository.GetProductsAsync();
        return products;
    }

    public async Task AddProductAsync(AddProductDto dto)
    {
        Product product = new Product
        {
            Name =  dto.Name,
            Price = dto.Price,
            Stock =  dto.Stock
        };
        await _unitOfWork.ProductRepository.AddProductAsync(product);
    }

    public async Task UpdateProductAsync(Guid id, UpdateProductDto dto)
    {
        var product = await _unitOfWork.ProductRepository.GetProductAsync(id);
        if(product == null) throw new Exception("Product not found");
        
        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Stock = dto.Stock;
        await _unitOfWork.ProductRepository.UpdateProductAsync(product);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await _unitOfWork.ProductRepository.GetProductAsync(id);
        if(product == null) throw new Exception("Product not found");
        await _unitOfWork.ProductRepository.DeleteProductAsync(id);
    }
}