namespace MarketAPI.Domain.Entities;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
}