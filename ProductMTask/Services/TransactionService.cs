using Microsoft.EntityFrameworkCore;
using ProductMTask.Data;
using ProductMTask.Dtos;
using ProductMTask.Models;

namespace ProductMTask.Services
{
    public class TransactionService : ITransationService
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionService(ApplicationDbContext dbContext)
        {
           _dbContext = dbContext;
        }
        public async Task Create(TransationDto dto)
        {
            var product = await  _dbContext.Products.FirstOrDefaultAsync(p=>p.Code==dto.Code);
            if (product is not null)
            {
                if (product.InitialQuantity >= dto.Quantity)
                {
                    var item = new Transaction()
                    {
                        Quantity = dto.Quantity,
                        ProductId = product.Id,
                        TotalPrice = product.Price * dto.Quantity,
                        Date= dto.Date
                    };

                    await _dbContext.Transactions.AddAsync(item);
                    product.InitialQuantity = product.InitialQuantity - dto.Quantity;
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task<List<TransactionViewModel>> GetTransations(DateTime? from, DateTime? to)
        {
            return await _dbContext.Transactions.Include(t=>t.Product).Where(t =>
                                                          (from == null || t.Date >= from)
                                                        && (to == null || t.Date <= to)

            ).OrderByDescending(t=>t.Date).Select(t => new TransactionViewModel 
            {
                
                Id=t.Id,
                Unit=t.Product.Unit,
                ProductId=t.ProductId,
                Quantity=t.Quantity,
                ProductCode=t.Product.Code,
                TotalPrice=t.TotalPrice,
                Date=t.Date,
                ProductName=t.Product.Name

            }
            ).ToListAsync();

        }
    }
}
