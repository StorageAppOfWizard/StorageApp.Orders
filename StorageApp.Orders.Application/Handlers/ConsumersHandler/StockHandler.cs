using MassTransit;
using Microsoft.Extensions.Logging;
using StorageApp.Orders.Application.Contracts;
using StorageApp.Orders.Domain.Contracts;
using StorageApp.Orders.Domain.Entity;
using StorageApp.Orders.Domain.Entity.MessagesEntity.Product;

namespace StorageApp.Orders.Application.Handlers.ConsumersHandler
{
    public class StockRejectedMessageHandler(ILogger logger, IOrderRepository orderRepository, IOrderHandler orderHandler) : IConsumer<StockRejectedMessage>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IEnumerable<IOrderHandler> _orderHandler;


        public async Task Consume(ConsumeContext<StockRejectedMessage> context)
        {
            var order = await _orderRepository.GetById(context.Message.OrderId);
            if (context.Message.StockAvailable is false)
            {
                logger.LogInformation("Mensagem Recebida, mudando o status do para rejeitado");

                var handler = _orderHandler.FirstOrDefault(x => x.TargetStatus == OrderStatus.Reject);
                await handler.Handle(order);
                await _orderRepository.CommitAsync();

            }

        }
    }
}
