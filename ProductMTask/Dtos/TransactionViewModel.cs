using ProductMTask.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductMTask.Dtos
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; } 
        public decimal TotalPrice { get; set; }
    }
}
