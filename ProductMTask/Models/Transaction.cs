using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductMTask.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public  Product Product { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="the")]
        public int Quantity { get; set; }
        public DateTime Date { get; set; } 
        public decimal TotalPrice { get; set; }
    }
}
