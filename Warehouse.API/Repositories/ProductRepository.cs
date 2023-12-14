using MongoDB.Driver;
using WarehouseAPI.Data.Interfaces;
using WarehouseAPI.Entities;
using WarehouseAPI.Repositories.Interfaces;

namespace WarehouseAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IWarehouseContext _context;

        public ProductRepository(IWarehouseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByGenre(string genre)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Genre, genre);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByTitle(string title)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Title, title);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
