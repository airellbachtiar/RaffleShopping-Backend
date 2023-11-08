using RaffleShopping.Services.Customers.Models;
using RaffleShopping.Services.Customers.Repositories;
using RaffleShopping.Services.Customers.Services;
using DotNetEnv.Configuration;

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

builder.Services.Configure<CustomerDatabaseSettings>(options =>
{
    options.ConnectionString = connectionString;
    options.DatabaseName = databaseName;
    options.CollectionName = collectionName;
});

// Add services to the container.
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<ICustomerService, CustomerService>();

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
