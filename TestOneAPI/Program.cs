using MassTransit;
using Shared.Refit;
using System.Reflection;
using Shared.RabbitMq;
using TestUserService.Sdk;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(Assembly.GetExecutingAssembly());
    x.UseRabbitMq(builder.Configuration.GetConnectionString("RabbitMq"));
});
builder.Services.AddMassTransitHostedService();

builder.Services
    .AddRefitClient<ITestUserService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5005"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
