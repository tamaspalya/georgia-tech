using MongoDB.Driver;
using WarehouseAPI.Data.Interfaces;
using WarehouseAPI.Entities;

namespace WarehouseAPI.Data
{
    public class WarehouseContext : IWarehouseContext
    {
        public WarehouseContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            WarehouseContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
