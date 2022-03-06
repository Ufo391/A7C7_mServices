using MassTransit;
using MassTransit.Definition;
using System.Reflection;
using TestPackages.MassTransit.RabbitMq;
using TestUserService.Sdk;
using Refit;
using TestTodoService.Sdk;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using TestApi.Hubs;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddMassTransit(x =>
    {
        x.AddConsumers(Assembly.GetExecutingAssembly());
        x.UseRabbitMq(builder.Configuration.GetConnectionString("RabbitMq"));
    })
    .AddMassTransitHostedService();

builder.Services
    .AddRefitClient<ITestTodoService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetConnectionString("TestTodoService")));

builder.Services
    .AddRefitClient<ITestUserService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetConnectionString("TestUserService")));

builder.Services.AddSignalR();

// Configure the HTTP request pipeline.
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
    {
        Predicate = (check) => check.Tags.Contains("ready"),
    });

    endpoints.MapHealthChecks("/health/live", new HealthCheckOptions());

    endpoints.MapControllers();
});

app.MapHub<TodoHub>("/hubs/todo");
app.MapHub<UserHub>("/hubs/user");

app.Run();
