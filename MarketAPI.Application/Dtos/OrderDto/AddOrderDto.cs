namespace MarketAPI.Application.Dtos.OrderDto;

public class AddOrderDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string NotificationType { get; set; }
}