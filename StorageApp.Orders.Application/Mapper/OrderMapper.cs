using StorageApp.Orders.Application.DTO;
using StorageApp.Orders.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageApp.Orders.Application.Mapper
{
    public static class OrderMapper
    {
        public static OrderDTO ToDTO(this Order order)
        {
            return new OrderDTO
            {
                Id = order.Id,
                ProductId = order.ProductId,
                Quantity = order.QuantityProduct,
                Status = order.Status,
                //UserId = order.UserId,
                //UserName = order.UserName,
                CreationDate = order.CreatedAt,

            };
        }

        public static Order ToEntity(this OrderDTO dto)
        {
            return new Order
            {
                ProductId = dto.ProductId,
                //UserId = dto.UserId,
                //UserName = dto.UserName,
                QuantityProduct = dto.Quantity

            };
        }

        public static Order ToEntity(this CreateOrderDTO dto,  string userId, string userName)
        {
            return new Order
            {
                Id = Guid.NewGuid(),
                ProductId = dto.ProductId,
                UserId = userId,
                UserName = userName,
                QuantityProduct = dto.Quantity,
            };
        }

        public static void ToEntity(this UpdateOrderDTO dto, Order order)
        {

            order.ProductId = dto.ProductId;
            order.QuantityProduct = dto.Quantity;
        }

        public static OrderMessage ToEntity(this Order order)
        {
            return new OrderMessage
            {
                Id = order.Id,
                ProductId = order.ProductId,
                QuantityProduct = order.QuantityProduct
            };
        }
    }
}
