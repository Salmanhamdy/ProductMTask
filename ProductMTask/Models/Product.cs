using System.ComponentModel.DataAnnotations;

namespace ProductMTask.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Unit { get; set; }
        [Range(0.1, double.MaxValue)]
        public decimal Price { get; set; }
        [Range(0,int.MaxValue)]
        public int InitialQuantity { get; set; }

        public ICollection<Transaction> Transactions=new HashSet<Transaction>();

    }
}
