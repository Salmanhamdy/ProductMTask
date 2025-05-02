using Microsoft.EntityFrameworkCore;
using ProductMTask.Models;
using ProductMTask.Dtos;

namespace ProductMTask.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; } 
        public DbSet<Transaction> Transactions { get; set; }

    }
}
