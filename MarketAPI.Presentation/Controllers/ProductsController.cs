using MarketAPI.Application.Dtos.ProductDto;
using MarketAPI.Application.Interfaces.IProduct;
using Microsoft.AspNetCore.Mvc;

namespace MarketAPI.Presentation.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductsController: ControllerBase
{
    private readonly IProductService _service;
    public  ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductByIdAsync(Guid id)
    {
        var product = await _service.GetProductAsync(id);
        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsAsync()
    {
        var products = await _service.GetProductsAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddProductAsync(AddProductDto dto)
    {
        await _service.AddProductAsync(dto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductAsync(Guid id, UpdateProductDto dto)
    {
        await _service.UpdateProductAsync(id, dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductAsync(Guid id)
    {
        await _service.DeleteProductAsync(id);
        return Ok();
    }
    
}