using RaffleShopping.Services.Customers.Models;
using RaffleShopping.Services.Customers.Repositories;
using RaffleShopping.Services.Customers.Services;
using DotNetEnv.Configuration;
using Microsoft.AspNetCore.Authentication;
using RaffleShopping.Services.Customers.Authentications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

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

//Add JWT Verification
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, (o) => { });

//authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EventOrganizer", policy => policy.RequireClaim("role", "EVENTORGANIZER"));
    options.AddPolicy("Customer", policy => policy.RequireClaim("role", "CUSTOMER"));
    options.AddPolicy("Admin", policy => policy.RequireClaim("role", "ADMIN"));
    options.AddPolicy("Public", policy =>
    {
        policy.RequireClaim("role", "ADMIN", "CUSTOMER", "EVENTORGANIZER");
    });
});

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

//Firebase
string firebaseConfigJson = Environment.GetEnvironmentVariable("FIREBASE_CONFIG_PATH");
builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromJson(firebaseConfigJson)
}));

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
