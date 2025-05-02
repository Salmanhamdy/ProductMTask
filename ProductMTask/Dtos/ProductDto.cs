using System.ComponentModel.DataAnnotations;

namespace ProductMTask.Dtos
{
    public class ProductDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Initial quantity can't be negative")]
        [Display(Name = "Quantity")]
        public int InitialQuantity { get; set; }
    }
}
