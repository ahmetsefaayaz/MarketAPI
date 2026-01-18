using MarketAPI.Application.Dtos.OrderDto;
using MarketAPI.Application.Interfaces.IOrder;
using MarketAPI.Application.Interfaces.IProduct;
using Microsoft.AspNetCore.Mvc;

namespace MarketAPI.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController: ControllerBase
{
    private readonly IOrderService _service;
    public OrdersController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderByIdAsync(Guid id)
    {
        var order = await _service.GetOrderAsync(id);
        return Ok(order);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrdersAsync()
    {
        var orders = await _service.GetOrdersAsync();
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync(AddOrderDto dto)
    {
        await _service.CreateOrderAsync(dto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderAsync(Guid id, UpdateOrderDto dto)
    {
        await _service.UpdateOrderAsync(id, dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderAsync(Guid id)
    {
        await _service.DeleteOrderAsync(id);
        return Ok();
    }
}