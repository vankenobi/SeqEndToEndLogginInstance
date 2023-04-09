using CustomerAPI.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;

var builder = WebApplication.CreateBuilder(args);

var loggerConfig = new LoggerConfiguration()
                    .WriteTo.Seq("http://localhost:5341")
                    .Enrich.WithProperty("Service", "CustomerAPI")
                    .Enrich.WithProperty("Timestamp", DateTimeOffset.UtcNow)
                    .MinimumLevel.Information()
                    .CreateLogger();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
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

app.UseMiddleware<LogHeaderMiddleWare>();

app.UseAuthorization();

app.MapControllers();

app.Run();

