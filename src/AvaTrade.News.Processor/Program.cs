using AvaTrade.News.Infrastructure.ExternalServices;
using AvaTrade.News.Processor.Services;
using AvaTrade.News.Domain.Repositories;
using AvaTrade.News.Infrastructure.Persistence;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<NewsProcessorService>();
builder.Services.AddSingleton<IPolygonNewsClient, MockPolygonNewsClient>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Add repository implementation when ready
// builder.Services.AddScoped<INewsRepository, NewsRepository>();

var host = builder.Build();
host.Run(); 