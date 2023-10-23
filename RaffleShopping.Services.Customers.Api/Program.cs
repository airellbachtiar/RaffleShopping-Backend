using RaffleShopping.Services.Customers.Models;
using RaffleShopping.Services.Customers.Repositories;
using RaffleShopping.Services.Customers.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                      });
});

// Configure Cosmos DB client
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString = configuration.GetSection("ConnectionStrings:ConnectionString").Value;
string databaseName = configuration.GetSection("ConnectionStrings:DatabaseName").Value;
string collectionName = configuration.GetSection("ConnectionStrings:CollectionName").Value;

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
