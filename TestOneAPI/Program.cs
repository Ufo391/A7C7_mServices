using MassTransit;
using Shared.Refit;
using System.Reflection;
using Shared.RabbitMq;
using TestTwoAPI.Sdk;
using Refit;
using TestOneAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ConnectionStringsOptions>(
    builder.Configuration.GetSection(ConnectionStringsOptions.ConnectionStrings));

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
