using MassTransit;
using Microsoft.Extensions.Logging;
using StorageApp.Orders.Application.Contracts;
using StorageApp.Orders.Domain.Contracts;
using StorageApp.Orders.Domain.Entity;
using StorageApp.Shared.Message.Storage;

namespace StorageApp.Orders.Application.Handlers.ConsumersHandler
{
    public class StockAcceptedMessageHandler(
        ILogger<StockAcceptedMessageHandler> logger,
        IOrderRepository orderRepository,
        IEnumerable<IOrderHandler> orderHandler) : IConsumer<StockAcceptedMessage>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IEnumerable<IOrderHandler> _orderHandler = orderHandler;

        public async Task Consume(ConsumeContext<StockAcceptedMessage> context)
        {
            var order = await _orderRepository.GetById(context.Message.OrderId);
            if (context.Message.StockAvailable is true)
            {
                logger.LogInformation("Mensagem Recebida, mudando o status do para rejeitado");

                var handler = _orderHandler.FirstOrDefault(x => x.TargetStatus == OrderStatus.Pending);
                var result = await handler.Handle(order);

                if (!result.IsSuccess)
                {
                    logger.LogError("Erro ao atualizar pedido: {Error}", result.Errors);
                    return;
                }
                await _orderRepository.CommitAsync();
                return;

            }

        }
    }
}
