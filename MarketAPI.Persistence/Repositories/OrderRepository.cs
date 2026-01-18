using MarketAPI.Application.Interfaces.IOrder;
using MarketAPI.Domain.Entities;
using MarketAPI.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace MarketAPI.Persistence.Repositories;

public class OrderRepository: IOrderRepository
{
    private readonly MarketAPIDbContext _context;

    public OrderRepository(MarketAPIDbContext context)
    {
        _context = context;
    }
    public async Task<Order?> GetOrderAsync(Guid orderId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        return order;
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        var orders = await _context.Orders.ToListAsync();
        return orders;
    }

    public async Task AddOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }
    public async Task DeleteOrderAsync(Guid orderId)
    {
        var  order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        _context.Orders.Remove(order);
    }

    public async Task UpdateOrderAsync(Order order)
    {
        _context.Orders.Update(order);
    }
}