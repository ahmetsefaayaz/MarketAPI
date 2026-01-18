using MarketAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketAPI.Persistence.DbContexts;

public class MarketAPIDbContext: DbContext
{
    public MarketAPIDbContext(DbContextOptions<MarketAPIDbContext> options) : base(options)
    {
    }
    public DbSet<Product>  Products { get; set; }
    public DbSet<Order> Orders { get; set; }
}