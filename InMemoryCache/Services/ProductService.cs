using InMemoryCache.Models;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCache.Services
{
    public class ProductService
    {
        private readonly IMemoryCache _cache;
        private static readonly string CacheKey = "ProductList";
        private static readonly List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 999.99m },
            new Product { Id = 2, Name = "Smartphone", Price = 499.99m },
            // Add more products as needed
        };

        public ProductService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public IEnumerable<Product> GetProducts()
        {
            if (!_cache.TryGetValue(CacheKey, out List<Product> productList))
            {
                // If not in cache, load data from the database (simulated here)
                productList = Products;

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(1),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                };

                _cache.Set(CacheKey, productList, cacheEntryOptions);
            }

            return productList;
        }
    }
}
