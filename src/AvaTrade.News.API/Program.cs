using AvaTrade.News.API.BackgroundServices;
using AvaTrade.News.Application.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MediatR
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(GetLatestNewsQuery).Assembly));

// Add repository implementation when ready
// builder.Services.AddScoped<INewsRepository, NewsRepository>();

// Add authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Configure JWT options
    });

// Add Outbox processor
builder.Services.AddHostedService<OutboxMessageProcessor>();

var app = builder.Build();

// ... rest of the configuration 

public partial class Program
{ }