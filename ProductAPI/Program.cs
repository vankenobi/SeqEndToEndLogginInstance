using ProductAPI.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var loggerConfig = new LoggerConfiguration()
            .WriteTo.Seq("http://localhost:5341")
            .Enrich.WithProperty("Service", "ProductAPI")
            .Enrich.WithProperty("Timestamp", DateTimeOffset.UtcNow)
            .MinimumLevel.Information()
            .CreateLogger();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddSerilog(loggerConfig);
builder.Services.AddSingleton<Serilog.ILogger>(loggerConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<LogHeaderMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

