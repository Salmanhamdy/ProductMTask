using Microsoft.EntityFrameworkCore;
using ProductMTask.Data;
using ProductMTask.Dtos;
using ProductMTask.Models;
using System.Threading.Tasks;

namespace ProductMTask.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private string GenerateUniqueProductCode()
        {
            int year = DateTime.Now.Year;
            int lastId =_dbContext.Products.OrderByDescending(p => p.Id).Select(p => p.Id).FirstOrDefault() + 1;
            int random = new Random().Next(10, 99); // Two-digit random number

            return $"{year}_{lastId}_{random}";
        }
        public async Task CreateAsync(ProductDto dto)
        {

            var product = new Product()
            {
                Price = dto.Price,
                Name = dto.Name,
                InitialQuantity = dto.InitialQuantity,
                Code = GenerateUniqueProductCode(),
                Unit=dto.Unit,

            };
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var products = await _dbContext.Products.ToListAsync();

            var productDtos = products.Select(p => new ProductDto
            {
                Name = p.Name,
                Unit = p.Unit,
                Price = p.Price,
                InitialQuantity = p.InitialQuantity,
                Code = p.Code
            }).ToList();

            return productDtos;
        }

        public async Task Update(ProductDto productdto)
        {
            var product = await GetProductByCodeAsync(productdto.Code);
            if(product is not null) 
            {
                product.Price = productdto.Price;
                product.Unit = productdto.Unit;
                product.Name = productdto.Name;
                product.InitialQuantity = productdto.InitialQuantity;
                _dbContext.Products.Update(product);
               await   _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Product?> GetProductByCodeAsync(string code)
        {
            return await _dbContext.Products.Where(p=>p.Code==code).FirstOrDefaultAsync();
        }
        public async Task<bool> DeleteAsync(string code)
        {
            var product = await _dbContext.Products
                .Where(p => p.Code == code)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return false;
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
