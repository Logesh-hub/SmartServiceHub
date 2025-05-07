using Grpc.Core;
using OrderService.Protos;

namespace OrderService.Services
{
    public class OrderGroupService: Protos.OrderService.OrderServiceBase
    {
        public override Task<OrderResponse> GetOrder(OrderRequest request, ServerCallContext context)
        {
            return Task.FromResult(new OrderResponse
            {
                OrderId = request.OrderId,
                Product = "Mjolnir Hammer",
                Price = 9999.99
            });
        }
    }
}
