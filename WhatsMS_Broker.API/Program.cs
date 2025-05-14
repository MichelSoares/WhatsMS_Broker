using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using WhatsMS_Broker.API.Configurations;
using WhatsMS_Broker.API.Interfaces;
using WhatsMS_Broker.API.Services;
using WhatsMS_Broker.Data.Context;

var builder = WebApplication.CreateBuilder(args);

var keySecret = builder.Configuration["Jwt:keySecret"];
var appAudience = builder.Configuration["Jwt:appAudience"];

var key = Encoding.ASCII.GetBytes(keySecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = appAudience,
        ValidAudience = appAudience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IClientWhatsMSService, ClientWhatsMSService>();
builder.Services.AddScoped<IMessageInbound, MessageInboundService>();
builder.Services.AddScoped<IMessageOutbound, MessageOutboundService>();
builder.Services.AddScoped<ILoginGeraToken, LoginGeraTokenService>();

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

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header usando o esquema Bearer.  
                      Exemplo: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

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
app.MapControllers();
app.Run();
