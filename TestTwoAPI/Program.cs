using Microsoft.EntityFrameworkCore;
using TestTwoAPI.Configurations;
using TestTwoAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ConnectionStringsOptions>(
    builder.Configuration.GetSection(ConnectionStringsOptions.ConnectionStrings));

// Replace with your connection string.
var cStringOptions = builder.Configuration.GetSection(ConnectionStringsOptions.ConnectionStrings).Get<ConnectionStringsOptions>();

// Replace with your server version and type.
// Use 'MariaDbServerVersion' for MariaDB.
// Alternatively, use 'ServerVersion.AutoDetect(connectionString)'.
// For common usages, see pull request #1233.
var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(cStringOptions.MySQL));

// Replace 'YourDbContext' with the name of your own DbContext derived class.
builder.Services.AddDbContext<UserDbContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(cStringOptions.MySQL, serverVersion)
        // The following three options help with debugging, but should
        // be changed or removed for production.
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    using var userDbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    userDbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
