using Loki.SharedKernel;
using MassTransit;

public class UserCreatedConsumer : IConsumer<UserCreated>
{
    private readonly ILogger<UserCreatedConsumer> _logger;

    public UserCreatedConsumer(ILogger<UserCreatedConsumer> logger)
    {
        _logger = logger;
    }
    public Task Consume(ConsumeContext<UserCreated> context)
    {
        var userCreated = context.Message;

        Console.WriteLine($"Console: User created: {userCreated.UserName} with ID {userCreated.UserId}");
        //_logger.LogInformation("Logger: User created: {UserName} with ID {UserId}", userCreated.UserName, userCreated.UserId);

        return Task.CompletedTask;
    }
}
