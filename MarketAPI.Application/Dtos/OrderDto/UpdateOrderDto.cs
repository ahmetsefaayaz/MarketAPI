namespace MarketAPI.Application.Dtos.OrderDto;

public class UpdateOrderDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}