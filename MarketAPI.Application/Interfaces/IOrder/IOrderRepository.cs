using MarketAPI.Application.Dtos.OrderDto;
using MarketAPI.Domain.Entities;

namespace MarketAPI.Application.Interfaces.IOrder;

public interface IOrderRepository
{
    Task<Order?> GetOrderAsync(Guid orderId);
    Task<List<Order>> GetOrdersAsync();
    Task AddOrderAsync(Order order);
    Task DeleteOrderAsync(Guid orderId);
    Task UpdateOrderAsync(Order order);
}