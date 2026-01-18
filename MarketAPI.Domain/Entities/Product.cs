namespace MarketAPI.Domain.Entities;

public class Product
{
    public Guid Id { get; set; } =  Guid.NewGuid();
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
}