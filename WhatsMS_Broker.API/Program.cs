using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using WhatsMS_Broker.API.Configurations;
using WhatsMS_Broker.API.Interfaces;
using WhatsMS_Broker.API.Services;
using WhatsMS_Broker.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IClientWhatsMSService, ClientWhatsMSService>();
builder.Services.AddScoped<IMessageInbound, MessageInboundService>();
builder.Services.AddScoped<IMessageOutbound, MessageOutboundService>();

var nodeBaseUrl = builder.Configuration["NodeApi:BaseUrl"];
builder.Services.AddHttpClient<IMessageSendAppNode, MessageSendAppNodeService>(client =>
{
    client.BaseAddress = new Uri(nodeBaseUrl);
});

builder.Services.AddDbContext<BrokerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    //.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Infinite)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WhatsMS Broker API",
        Version = "v1",
        Description = "API para envio e recebimento de mensagens via WhatsApp"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WhatsMS_Broker_API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
