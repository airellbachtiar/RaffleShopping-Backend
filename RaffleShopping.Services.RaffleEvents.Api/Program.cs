using Azure.Messaging.ServiceBus;
using DotNetEnv.Configuration;
using RaffleShoppping.Services.RaffleEvents.EventProcessors;
using RaffleShoppping.Services.RaffleEvents.Models;
using RaffleShoppping.Services.RaffleEvents.ServiceBus;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
});
// Add services to the container.

builder.Services.AddControllers();

//Load Env file
DotNetEnv.Env.Load();

//Add MongoDB & RabbitMQ
var config = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddDotNetEnv()
    .Build();

var serviceBusConnectionString = config.GetValue<string>("SERVICE_BUS_CONNECTION_STRING");
var serviceBusQueueName = config.GetValue<string>("SERVICE_BUS_QUEUE_NAME");

//Event Processor
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();

//Add MessageSubscriber
builder.Services.AddHostedService<ServiceBusSubscriber>();

builder.Services.Configure<AzureServiceBusSettings>(options =>
{
    options.ServiceBusClient = new ServiceBusClient(serviceBusConnectionString,
        new ServiceBusClientOptions()
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        });
    options.QueueName = serviceBusQueueName;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
