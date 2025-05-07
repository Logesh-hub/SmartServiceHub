using MassTransit;
using OrderService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Sends output to container logs
builder.Logging.SetMinimumLevel(LogLevel.Information);
builder.Services.AddControllers();
builder.Services.AddGrpc();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserCreatedConsumer>();
    x.UsingRabbitMq((context, configure) =>
    {
        configure.Host("rabbitmq", "/", cfg =>
        {
            cfg.Username("guest");
            cfg.Password("guest");
        });

        configure.ReceiveEndpoint("user-service-queue", endpoint =>
        {
            endpoint.ConfigureConsumer<UserCreatedConsumer>(context);
        });
        configure.UseMessageRetry(retryConfig =>  // Retry logic
        {
            retryConfig.Interval(3, TimeSpan.FromSeconds(5));
        });
    });


});
var app = builder.Build();

app.MapControllers();
app.MapGet("/api/Order/{id}", (string id) => new { Id = id, Product = "Mjolnir Hammer", Price = 9999.99 });
app.MapGet("/", () => "OrderService is running 🚀");

app.MapGrpcService<OrderGroupService>();

app.Run();
