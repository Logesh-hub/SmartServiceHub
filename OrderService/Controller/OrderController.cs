using Loki.SharedKernel;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IPublishEndpoint _endpoint;
        public OrderController(IPublishEndpoint publishEndpoint)
        {
            _endpoint = publishEndpoint;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest createOrder)
        {
            var orderId = Guid.NewGuid();

            await _endpoint.Publish(new OrderCreated
            {
                OrderId = orderId,
                UserId = createOrder.UserId,
                Amount = createOrder.Amount
            });

            return Ok(new {Message = "OrderCreated",OrderId = orderId});
        }
    }

    public class CreateOrderRequest
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
    }
}

