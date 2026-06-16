using Microsoft.EntityFrameworkCore;
using pratice_aegona_v2.Data;
using pratice_aegona_v2.Models.Entities;
using pratice_aegona_v2.Models.ViewModels;
using pratice_aegona_v2.Services.Interfaces;

namespace pratice_aegona_v2.Services.Implementations
{
    public class ProductService(AppDbContext context) : IProductService
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<ProductResponseViewModel>> GetAllProductsAsync()
        {
            return await _context.Products
                .Select(p => new ProductResponseViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    ImageUrl = p.ImageUrl,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<ProductResponseViewModel?> GetProductByIdAsync(Guid id)
        {
            var p = await _context.Products.FindAsync(id);
            if (p == null) return null;

            return new ProductResponseViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                ImageUrl = p.ImageUrl,
                CreatedAt = p.CreatedAt
            };
        }

        public async Task<ProductResponseViewModel> CreateProductAsync(CreateProductViewModel model)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                StockQuantity = model.StockQuantity,
                ImageUrl = model.ImageUrl,
                CreatedAt = DateTime.UtcNow
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductResponseViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ImageUrl = product.ImageUrl,
                CreatedAt = product.CreatedAt
            };
        }

        public async Task<ProductResponseViewModel?> UpdateProductAsync(Guid id, UpdateProductViewModel model)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.StockQuantity = model.StockQuantity;
            product.ImageUrl = model.ImageUrl;

            await _context.SaveChangesAsync();

            return new ProductResponseViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ImageUrl = product.ImageUrl,
                CreatedAt = product.CreatedAt
            };
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}