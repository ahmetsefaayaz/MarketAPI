using MarketAPI.Application.Dtos.ProductDto;
using MarketAPI.Application.Interfaces.IProduct;
using MarketAPI.Application.Interfaces.IUnitOfWork;
using MarketAPI.Application.Services;
using MarketAPI.Domain.Entities;
using Moq;

namespace ProductServiceTest;

public class ProductServiceTest
{
    private readonly IProductService _productService;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    public ProductServiceTest()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _productService = new ProductService(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task GetProductById_ShouldReturnProduct_WhenProductExists()
    {
        var productId = Guid.NewGuid();
        Product product = new Product {Id = productId,  Name = "Test", Price = 500, Stock = 30};
        _mockUnitOfWork.Setup(x => x.ProductRepository.GetProductAsync(productId)).ReturnsAsync(product);
        var newProduct = await _productService.GetProductAsync(productId);
        Assert.NotNull(newProduct);
        Assert.Equal(newProduct.Id, productId);
        Assert.Equal(newProduct.Name, product.Name);
        Assert.Equal(newProduct.Price, product.Price);
    }

    [Fact]
    public async Task GetProductById_ShouldReturnException_WhenProductDoesNotExist()
    {
        var productId = Guid.NewGuid();
        _mockUnitOfWork.Setup(x => x.ProductRepository.GetProductAsync(productId)).ReturnsAsync((Product)null);
        Assert.ThrowsAsync<Exception>(async () => await _productService.GetProductAsync(productId));
    }

    [Fact]
    public async Task CreateProduct_ShouldCreateProduct_WhenDtoIsValid()
    {
        AddProductDto dto = new AddProductDto {Name = "Test", Price = 500, Stock = 30};
        _mockUnitOfWork.Setup(x => x.ProductRepository.AddProductAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
        await _productService.AddProductAsync(dto);
        _mockUnitOfWork.Verify(x => x.ProductRepository.AddProductAsync(It.Is<Product>(p =>
            p.Name == dto.Name &&
            p.Price == dto.Price &&
            p.Stock == dto.Stock
            )), Times.Once);
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task UpdateProduct_ShouldUpdateProduct_WhenDtoAndIdIsValid()
    {
        var productId = Guid.NewGuid();
        
        var product = new Product
        {
            Id = productId,
            Name = "Test",
            Price = 500,
            Stock = 30
        };
        var dto = new UpdateProductDto
        {
            Name = "New Test",
            Price = 555,
            Stock = 80
        };
        _mockUnitOfWork.Setup(x => x.ProductRepository.GetProductAsync(productId)).ReturnsAsync(product);
        _mockUnitOfWork.Setup(x => x.ProductRepository.UpdateProductAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
        
        await _productService.UpdateProductAsync(productId, dto);
        //Test - 1
        _mockUnitOfWork.Verify(x => x.ProductRepository.UpdateProductAsync(It.Is<Product>(p => 
            p.Id == productId &&
            p.Name == dto.Name &&
            p.Stock == dto.Stock &&
            p.Price == dto.Price
            )),  Times.Once);
        //Test - 2
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
        
    }
    
    
}