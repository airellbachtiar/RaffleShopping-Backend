using RaffleShopping.Services.Catalogs.Models;
using RaffleShopping.Services.Catalogs.Repositories;
using RaffleShopping.Services.Catalogs.Services;
using DotNetEnv.Configuration;
using Azure.Messaging.ServiceBus;
using RaffleShopping.Services.Catalogs.ServiceBus;

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

// Configure Cosmos DB client
//Load Env file
DotNetEnv.Env.Load();

//Add MongoDB
var config = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddDotNetEnv()
    .Build();

string connectionString = config.GetValue<string>("CONNECTION_STRING");
string databaseName = config.GetValue<string>("DATABASE_NAME");
string collectionName = config.GetValue<string>("COLLECTION_NAME");
var serviceBusConnectionString = config.GetValue<string>("SERVICE_BUS_CONNECTION_STRING");
var serviceBusQueueName = config.GetValue<string>("SERVICE_BUS_QUEUE_NAME");
string blobStorageConnectionString = config.GetValue<string>("BLOB_STORAGE_CONNECTION_STRING");
string blobStorageContainerName = config.GetValue<string>("BLOB_STORAGE_CONTAINER_NAME");
string blobStorageAccountName = config.GetValue<string>("BLOB_STORAGE_ACCOUNT_NAME");

builder.Services.Configure<CatalogDatabaseSettings>(options =>
{
    options.ConnectionString = connectionString;
    options.DatabaseName = databaseName;
    options.CollectionName = collectionName;
});

builder.Services.Configure<CatalogBlobStorageSettings>(options =>
{
    options.ConnectionString = blobStorageConnectionString;
    options.ContainerName = blobStorageContainerName;
    options.AccountName = blobStorageAccountName;
});

builder.Services.Configure((AzureServiceBusSettings options) =>
{
    options.ServiceBusClient = new ServiceBusClient(serviceBusConnectionString,
        new ServiceBusClientOptions()
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        });
    options.QueueName = serviceBusQueueName;
});

// Add services to the container.
builder.Services.AddSingleton<ICatalogRepository, CatalogRepository>();
builder.Services.AddSingleton<ICatalogBlobStorage, CatalogBlobStorage>();
builder.Services.AddSingleton<ICatalogServices, CatalogServices>();
builder.Services.AddSingleton<ICatalogServiceBusClient, CatalogServiceBusClient>();

builder.Services.AddControllers();
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
