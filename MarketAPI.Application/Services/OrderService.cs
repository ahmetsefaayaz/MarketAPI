using MarketAPI.Application.Dtos.OrderDto;
using MarketAPI.Application.Interfaces.IOrder;
using MarketAPI.Application.Interfaces.IUnitOfWork;
using MarketAPI.Application.Services.Notifications;
using MarketAPI.Domain.Entities;

namespace MarketAPI.Application.Services;

public class OrderService: IOrderService
{
    private readonly IUnitOfWork  _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<Order> GetOrderAsync(Guid orderId)
    {
        var order = await _unitOfWork.OrderRepository.GetOrderAsync(orderId);
        if(order == null) throw new Exception("Order not found");
        return order;
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        return await _unitOfWork.OrderRepository.GetOrdersAsync();
    }

    public async Task CreateOrderAsync(AddOrderDto dto)
    {
        var product = await _unitOfWork.ProductRepository.GetProductAsync(dto.ProductId);
        if(product == null) throw new Exception("Product not found");
        if(product.Stock < dto.Quantity)  throw new Exception("Not enough stock");
        product.Stock -= dto.Quantity;
        await _unitOfWork.ProductRepository.UpdateProductAsync(product);
        
        Order order = new Order
        {
            ProductId =  dto.ProductId,
            Quantity = dto.Quantity
        };
        await _unitOfWork.OrderRepository.AddOrderAsync(order);
        
        
        await _unitOfWork.CompleteAsync();

        try
        {
            var notificationService = NotificationFactory.Create(dto.NotificationType);
            await notificationService.SendAsync($"Siparişiniz alındı! sipariş no: {order.Id}");

        }
        catch(Exception ex)
        {
            Console.WriteLine("Bildirim gönderilemedi :"  + ex.Message);
        }
        
    }

    public async Task UpdateOrderAsync(Guid orderId, UpdateOrderDto dto)
    {
        var order = await _unitOfWork.OrderRepository.GetOrderAsync(orderId);
        if (order == null) throw new Exception("Order not found");

        if (order.ProductId != dto.ProductId)
        {
            var oldProduct = await _unitOfWork.ProductRepository.GetProductAsync(order.ProductId);
            if (oldProduct != null)
            {
                oldProduct.Stock += order.Quantity;
                await _unitOfWork.ProductRepository.UpdateProductAsync(oldProduct);
            }

            var newProduct = await _unitOfWork.ProductRepository.GetProductAsync(dto.ProductId);
            if (newProduct == null) throw new Exception("New product not found");
            if (newProduct.Stock < dto.Quantity) throw new Exception("Not enough stock for new product");

            newProduct.Stock -= dto.Quantity;
            await _unitOfWork.ProductRepository.UpdateProductAsync(newProduct);
        }
        else
        {
            var product = await _unitOfWork.ProductRepository.GetProductAsync(dto.ProductId);

            int quantityDifference = dto.Quantity - order.Quantity;

            if (quantityDifference > 0 && product.Stock < quantityDifference)
            {
                throw new Exception("Not enough stock");
            }

            product.Stock -= quantityDifference;
            await _unitOfWork.ProductRepository.UpdateProductAsync(product);
        }
        order.ProductId = dto.ProductId;
        order.Quantity = dto.Quantity;
        await _unitOfWork.OrderRepository.UpdateOrderAsync(order);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteOrderAsync(Guid orderId)
    {
        var order = await _unitOfWork.OrderRepository.GetOrderAsync(orderId);
        if(order == null) throw new Exception("Order not found");
        var product = await _unitOfWork.ProductRepository.GetProductAsync(order.ProductId);
        if(product == null) throw new Exception("Product not found");
        
        product.Stock += order.Quantity;
        await _unitOfWork.ProductRepository.UpdateProductAsync(product);
        
        
        await _unitOfWork.OrderRepository.DeleteOrderAsync(orderId);
        await _unitOfWork.CompleteAsync();
    }
}