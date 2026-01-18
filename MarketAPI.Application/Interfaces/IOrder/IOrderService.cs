using MarketAPI.Application.Dtos.OrderDto;
using MarketAPI.Domain.Entities;

namespace MarketAPI.Application.Interfaces.IOrder;

public interface IOrderService
{
    Task <Order> GetOrderAsync(Guid orderId);
    Task <List<Order>> GetOrdersAsync();
    Task CreateOrderAsync(AddOrderDto dto);
    Task UpdateOrderAsync(Guid orderId, UpdateOrderDto dto);
    Task  DeleteOrderAsync(Guid orderId);
}