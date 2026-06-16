using pratice_aegona_v2.Models.ViewModels;

namespace pratice_aegona_v2.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseViewModel>> GetAllProductsAsync();
        Task<ProductResponseViewModel?> GetProductByIdAsync(Guid id);
        Task<ProductResponseViewModel> CreateProductAsync(CreateProductViewModel model);
        Task<ProductResponseViewModel?> UpdateProductAsync(Guid id, UpdateProductViewModel model);
        Task<bool> DeleteProductAsync(Guid id);
    }
}