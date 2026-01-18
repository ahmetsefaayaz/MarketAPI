using MarketAPI.Application.Interfaces.IOrder;
using MarketAPI.Application.Interfaces.IProduct;
using MarketAPI.Application.Interfaces.IUnitOfWork;
using MarketAPI.Persistence.DbContexts;

namespace MarketAPI.Persistence.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly MarketAPIDbContext _context;

    public UnitOfWork(MarketAPIDbContext context)
    {
        _context = context;
        OrderRepository = new OrderRepository(_context);
        ProductRepository = new ProductRepository(_context);
    }
    
    
    public IOrderRepository OrderRepository { get; private set; }
    public IProductRepository ProductRepository { get; private set; }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }


    public void Dispose()
    {
        _context.Dispose();
    }
}
