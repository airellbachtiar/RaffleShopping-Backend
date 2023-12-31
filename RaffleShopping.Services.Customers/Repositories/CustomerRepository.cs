﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RaffleShopping.Services.Customers.Models;
using System.Security.Authentication;

namespace RaffleShopping.Services.Customers.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customerCollection;

        public CustomerRepository(IOptions<CustomerDatabaseSettings> settings)
        {
            string connectionString = settings.Value.ConnectionString;
            MongoClientSettings mongoSettings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            mongoSettings.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(mongoSettings);

            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _customerCollection = mongoDatabase.GetCollection <Customer>(settings.Value.CollectionName);
        }

        public async Task AddUserAsync(Customer customer)
        {
            await _customerCollection.InsertOneAsync(customer);
        }

        public async Task<Customer> GetUserByEmailAsync(string email)
        {
            Customer user = await _customerCollection.Find(u => u.Email == email).FirstOrDefaultAsync();
            return user;
        }

        public async Task DeleteUserAsync(string customerId)
        {
            await _customerCollection.DeleteOneAsync(customerId);
        }
    }
}
