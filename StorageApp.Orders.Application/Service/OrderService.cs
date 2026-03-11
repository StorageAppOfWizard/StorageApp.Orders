using Ardalis.Result;
using StorageApp.Orders.Application.Contracts;
using StorageApp.Orders.Application.DTO;
using StorageApp.Orders.Application.Mapper;
using StorageApp.Orders.Domain.Contracts;
using StorageApp.Orders.Domain.Entity;

namespace StorageApp.Orders.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IEnumerable<IOrderHandler> _orderHandler;
        private readonly IMessageProducer _messageProducer;
        private readonly IOrderRepository _orderRepository;


        public OrderService(IMessageProducer messageProducer, IOrderRepository orderRepository)
        {
            _messageProducer = messageProducer;
            _orderRepository = orderRepository;
        }

        public async Task<Result<PagedItems<OrderDTO>>> GetAllAsync(int page, int pageQuantity)
        {
            var order = await _orderRepository.GetOrderWithIncludes();
            if (order is null)
                return Result.Success();

            var dtoList = order.Select(o => o.ToDTO()).ToList();
            var pagedList = new PagedItems<OrderDTO>(dtoList, page, pageQuantity);

            return Result.Success(pagedList);
        }

        public async Task<Result<OrderDTO>> GetByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetById(id);
            if (order is null)
                return Result.NotFound("Order not found");

            return Result.Success(order.ToDTO());
        }

        //public async Task<Result<List<OrderDTO>>> GetOrdersByUserIdAsync(int page, int pageQuantity)
        //{

        //    if (_userContextAuth.IsAuthenticated is false) return Result.Forbidden();

        //    var orders = await _orderRepository.GetOrdersByUserId(page, pageQuantity, _userContextAuth.UserId);

        //    if (orders is null || !orders.Any())
        //        return Result.Success();

        //    return Result.Success(orders.Select(o => o.ToDTO()).ToList());
        //}

        public async Task<Result> CreateOrderAsync(CreateOrderDTO dto)
        {

            //var existingProduct = await _orderRepository.GetProductById(dto.ProductId);
            //if (existingProduct is null)
            //    return Result.NotFound("Not Found Product, check if the product exist");

            //if (existingProduct.Quantity < dto.Quantity)
            //    return Result.Error("There is not sufficient quantity for this order");

            //var userId = _userContextAuth.UserId;
            //var username = _userContextAuth.UserName;

            var userId = "teste";
            var username = "testeee";

            //if (userId is null)
            //    return Result.Unauthorized("Sign in for create a order");
            int fakequantity = 50;

            fakequantity -= dto.Quantity;
            var body = dto.ToEntity(userId, username);
            await _orderRepository.Create(body);

            await _orderRepository.CommitAsync();
            await _messageProducer.SendMessage(body);
            return Result.SuccessWithMessage("Order Created");
        }

        //public async Task<Result> RejectOrderAsync(Guid orderId)
        //{
        //    var order = await _orderRepository.GetById(orderId);
        //    var product = await _orderRepository.ProductRepository.GetById(order.ProductId);

        //    if (order is null)
        //        return Result.NotFound("Not Found Order");

        //    var handler = _orderHandler.FirstOrDefault(x => x.TargetStatus == OrderStatus.Reject);

        //    if (handler is null)
        //        return Result.Error($"There's not handler for status ${order.Status}");

        //    var result = await handler.Handle(order, product);

        //    if (!result.IsSuccess) return Result.Error(result.Errors.FirstOrDefault());

        //    await _orderRepository.CommitAsync();
        //    return Result.SuccessWithMessage("Order Rejected");
        //}

        //public async Task<Result> ApproveOrderAsync(Guid orderId)
        //{
        //    var order = await _orderRepository.GetById(orderId);

        //    if (order is null)
        //        return Result.NotFound("Not Found Order");

        //    var handler = _orderHandler
        //         .FirstOrDefault(x => x.TargetStatus == OrderStatus.Approved);

        //    if (handler is null)
        //        return Result.Error($"There's not handler for status ${order.Status}");

        //    var result = await handler.Handle(order, null);

        //    if (!result.IsSuccess) return Result.Error(result.Errors.FirstOrDefault());

        //    _orderRepository.Update(order);
        //    await _orderRepository.CommitAsync();

        //    return Result.SuccessWithMessage("Order Approved");
        //}

        //public async Task<Result> DeleteOrderAsync(Guid id)
        //{
        //    var order = await _orderRepository.GetById(id);
        //    var product = await _orderRepository.ProductRepository.GetById(order.ProductId);

        //    if (order is null)
        //        return Result.NotFound("Order not exist");

        //    var handler = _orderHandler
        //        .FirstOrDefault(x => x.TargetStatus == OrderStatus.Reject);

        //    if (handler is null)
        //        return Result.Error($"There's not handler for status ${order.Status}");

        //    await handler.Handle(order, product);

        //    _orderRepository.Delete(order);
        //    await _orderRepository.CommitAsync();

        //    return Result.SuccessWithMessage("Order deleted successfully");
        //}

        //public async Task<Result<OrderStatus>> CancelOrderAsync(Guid orderId, OrderStatus status)
        //{
        //    var order = await _orderRepository.GetById(orderId);
        //    if (order is null)
        //        return Result.NotFound("Not Found Order");

        //    if (order.Status is not OrderStatus.Pending)
        //        return Result.Error("You can to cancel only pending order");

        //    await RestoreProductStock(order.ToDTO());
        //    order.UpdateStatus(status);
        //    return Result.Success();
        //}

        public async Task<Result> UpdateOrderAsync(UpdateOrderDTO dto)
        {
            var order = await _orderRepository.GetById(dto.Id);
            if (order is null)
                return Result.NotFound("Order Not Found");

            if (order.Status == OrderStatus.Approved)
                return Result.Error("You can't update one order already approved");

            dto.ToEntity(order);

            await _orderRepository.CommitAsync();
            return Result.SuccessWithMessage("Order Updated");
        }


        //private async Task RestoreProductStock(OrderDTO order)
        //{
        //    var product = await _orderRepository.ProductRepository.GetById(order.ProductId);
        //    if (product is null) return;

        //    product.Quantity += order.Quantity;

        //}
    }
}
