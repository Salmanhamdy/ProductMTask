using ProductMTask.Dtos;
using ProductMTask.Models;

namespace ProductMTask.Services
{
    public interface IProductService
    {
        Task CreateAsync(ProductDto dto);
        Task Update(ProductDto productdto);
        Task< List<ProductDto?>> GetProductsAsync();
        Task<Product?> GetProductByCodeAsync(string code);
        Task<bool> DeleteAsync(string code);
    }
}
