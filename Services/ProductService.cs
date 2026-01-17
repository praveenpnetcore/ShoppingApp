using Microsoft.EntityFrameworkCore;
using ShoppingApp.Models;
using ShoppingApp.Repositories;

namespace ShoppingApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.Query()
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            return await _productRepository.Query()
                .Include(p => p.Category)
                .Where(p => p.IsActive && p.StockQuantity > 0)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _productRepository.Query()
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId && p.IsActive)
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _productRepository.Query()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            product.CreatedDate = DateTime.Now;
            return await _productRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            product.UpdatedDate = DateTime.Now;
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
            {
                product.IsActive = false;
                await _productRepository.UpdateAsync(product);
            }
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetActiveProductsAsync();

            return await _productRepository.Query()
                .Include(p => p.Category)
                .Where(p => p.IsActive &&
                    (p.Name.Contains(searchTerm) ||
                     p.Description.Contains(searchTerm) ||
                     p.Category!.Name.Contains(searchTerm)))
                .ToListAsync();
        }
    }
}
