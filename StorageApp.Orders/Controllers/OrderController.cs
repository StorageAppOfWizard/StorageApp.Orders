using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorageApp.Orders.Application.Contracts;
using StorageApp.Orders.Application.DTO;
using StorageApp.Orders.Web;
using System.ComponentModel;

namespace StorageApp.Orders.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;

        }

        //[Authorize(Policy = "AdminOrManager")]
        //[HttpPatch("reject-order/{id:Guid}")]
        //public async Task<IActionResult> RejectOrder(Guid id)
        //{
        //    var result = await _orderService.RejectOrderAsync(id);
        //    return result.ToActionResult();
        //}

        //[Authorize(Policy = "AdminOrManager")]
        //[HttpPatch("approve-order/{id:Guid}")]
        //public async Task<IActionResult> ApproveOrder(Guid id)
        //{
        //    var result = await _orderService.ApproveOrderAsync(id);
        //    return result.ToActionResult();
        //}

        [Authorize]
        [HttpPost("create-order")]
        public async Task<IActionResult> Create([FromBody] CreateOrderDTO order)
        {

            var result = await _orderService.CreateOrderAsync(order);
            return result.ToActionResult();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery, DefaultValue(1)] int page,
            [FromQuery, DefaultValue(20)] int pageQuantity)
        {
            var result = await _orderService.GetAllAsync(page, pageQuantity);
            return result.ToActionResult();

        }

        [Authorize]
        [HttpGet("my-orders")]
        public async Task<IActionResult> GetByUser(
            [FromQuery, DefaultValue(1)] int page,
            [FromQuery, DefaultValue(20)] int pageQuantity)
        {
            var result = await _orderService.GetOrdersByUserIdAsync(page, pageQuantity);
            return result.ToActionResult();

        }


        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _orderService.GetByIdAsync(id);
            return result.ToActionResult();
        }

        //[Authorize(Policy = "AdminOrManager")]
        //[HttpDelete("{id:Guid}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var result = await _orderService.DeleteOrderAsync(id);
        //    return result.ToActionResult();
        //}

    }
}
