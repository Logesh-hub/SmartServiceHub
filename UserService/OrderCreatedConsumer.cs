using Loki.SharedKernel;
using MassTransit;

namespace UserService
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        public Task Consume(ConsumeContext<OrderCreated> context)
        {
            var msg = context.Message;

            Console.WriteLine($"📦 OrderCreated received: OrderId={msg.OrderId}, UserId={msg.UserId}, Amount={msg.Amount}");
            return Task.CompletedTask;
        }
    }
}
