using Ardalis.Result;
using StorageApp.Orders.Application.DTO;
using StorageApp.Orders.Domain.Entity;

namespace StorageApp.Orders.Application.Contracts
{
    public interface IOrderService
    {
        //Task<Result> RejectOrderAsync(Guid orderId);
        //Task<Result> ApproveOrderAsync(Guid orderId);
        Task<Result> CreateOrderAsync(CreateOrderDTO dto);
        //Task<Result<PagedItems<OrderDTO>>> GetOrdersByUserIdAsync(int page, int pageQuantity);
        //Task<Result> DeleteOrderAsync(Guid id);
        Task<Result<OrderDTO>> GetByIdAsync(Guid id);
        Task<Result<PagedItems<OrderDTO>>> GetAllAsync(int page, int pageQuantity);

    }
}
