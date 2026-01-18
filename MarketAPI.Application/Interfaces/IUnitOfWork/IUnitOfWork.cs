using MarketAPI.Application.Interfaces.IOrder;
using MarketAPI.Application.Interfaces.IProduct;

namespace MarketAPI.Application.Interfaces.IUnitOfWork;

public interface IUnitOfWork: IDisposable
{
    IOrderRepository OrderRepository { get; }
    IProductRepository ProductRepository { get; }
    Task<int> CompleteAsync();
}