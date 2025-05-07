using Loki.SharedKernel;
using MassTransit;
using UserService;
using UserService.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq","/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("order-service-queue", e =>
        {
            e.ConfigureConsumer<OrderCreatedConsumer>(context);
        });
        cfg.UseMessageRetry(retryConfig =>  // Retry logic
        {
            retryConfig.Interval(3,TimeSpan.FromSeconds(5));
        });
    });
});


var app = builder.Build();

// Minimal API route
app.MapPost("/api/user", async (UserCreateRequest request, IPublishEndpoint publishEndpoint) =>
{
    var userId = Guid.NewGuid().ToString();

    await publishEndpoint.Publish(new UserCreated
    {
        UserId = new Guid(userId),
        UserName = request.userName

    });

    return Results.Ok(new { UserId = userId, UserName = request.userName });
});

app.MapGet("/api/user/{id}", (string id) => new { Id = id, Name = "Lokimon" });

app.MapGet("/", () => "UserService is running 🚀");
app.MapGrpcService<UserGroupService>();

app.Run();

record UserCreateRequest(string userName);