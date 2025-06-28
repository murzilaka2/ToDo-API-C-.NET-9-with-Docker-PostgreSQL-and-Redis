using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // ћаксимальное количество повторных попыток
            maxRetryDelay: TimeSpan.FromSeconds(30), // ћаксимальное врем€ ожидани€ между попытками
            errorCodesToAdd: null // null или пустой список - использует стандартные ошибки дл€ повтора
        );
    });
});

// Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(builder.Configuration["Redis:Configuration"]));
builder.Services.AddSingleton<RedisCacheService>();

var app = builder.Build();

// Migrate and seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    db.Database.Migrate();
    if (!db.Tasks.Any())
    {
        db.Tasks.AddRange(
            new TaskItem { Title = "Sample task 1" },
            new TaskItem { Title = "Sample task 2", Description = "Details..." }
        );
        db.SaveChanges();
    }
}

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // отключено дл€ Docker (HTTP only)
app.MapControllers();
app.Run();