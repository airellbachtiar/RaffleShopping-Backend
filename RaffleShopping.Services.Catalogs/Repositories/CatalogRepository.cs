using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RaffleShopping.Services.Catalogs.Models;
using System.Security.Authentication;

namespace RaffleShopping.Services.Catalogs.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private IMongoCollection<Catalog> _catalogCollection;

        public CatalogRepository(IOptions<CatalogDatabaseSettings> settings)
        {
            string connectionString = settings.Value.ConnectionString;
            MongoClientSettings mongoSettings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            mongoSettings.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(mongoSettings);

            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _catalogCollection = mongoDatabase.GetCollection<Catalog>(settings.Value.CollectionName);
        }

        public void AddCatalogAsync(Catalog catalog)
        {
            _catalogCollection.InsertOneAsync(catalog);
        }
    }
}
