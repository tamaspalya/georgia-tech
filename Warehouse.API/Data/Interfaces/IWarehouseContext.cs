using MongoDB.Driver;
using WarehouseAPI.Entities;

namespace WarehouseAPI.Data.Interfaces
{
    public interface IWarehouseContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
