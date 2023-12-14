using MongoDB.Driver;
using WarehouseAPI.Entities;

namespace WarehouseAPI.Data
{
    public static class WarehouseContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool productExists = productCollection.Find(p => true).Any();
            if (!productExists)
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product
                {
                    Id = "602d2149e773f2a3990b47f5",
                    ISBN = "9781156923443",
                    Title = "Harry Potter: The Prequel",
                    Author = "J.K. Rowling",
                    Publisher = "Blackrock",
                    PublicationYear = 2008,
                    Price = 19.99m,
                    Genre = "Fantasy",
                    Language = "English",
                    Description = "A very nice prequel about the magic wand boy",
                    SellerId = "602d21491233f2a3990b47f5",
                    Quantity = 5
                },
                new Product
                {
                    Id = "501c2783f34a1b0987ac1234",
                    ISBN = "9780234567891",
                    Title = "The Lost City of Zerzura",
                    Author = "Eleanor M. Hightower",
                    Publisher = "Emerald Press",
                    PublicationYear = 2015,
                    Price = 24.99m,
                    Genre = "Adventure",
                    Language = "English",
                    Description = "An exhilarating tale of discovery and mystery in the heart of Africa.",
                    SellerId = "501c2783ab45b10987ac1234",
                    Quantity = 8
                },
                new Product
                {
                    Id = "602d2149e773f2a3990b47f6",
                    ISBN = "9781122334455",
                    Title = "Stars Beyond the Horizon",
                    Author = "Marcus L. Stellar",
                    Publisher = "Galaxy Publishing",
                    PublicationYear = 2020,
                    Price = 29.99m,
                    Genre = "Science Fiction",
                    Language = "English",
                    Description = "An epic journey through space, revealing the mysteries of the cosmos.",
                    SellerId = "602d21491233f2a3990b47f6",
                    Quantity = 10
                }
            };
        }
    }
}
