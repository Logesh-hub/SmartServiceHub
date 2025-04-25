using UserService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
var app = builder.Build();

// Minimal API route
app.MapGet("/api/user/{id}", (string id) => new { Id = id, Name = "Lokimon" });

app.MapGrpcService<UserGroupService>();
app.MapGet("/", () => "UserService is running 🚀");

app.Run();
