using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Category ID is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        public string Weight { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Unit price must be greater than or equal to 0")]
        public decimal UnitPrice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Units in stock must be greater than or equal to 0")]
        public int UnitsInStock { get; set; }

        [StringLength(500, ErrorMessage = "Image URL cannot exceed 500 characters")]
        public string ImageUrl { get; set; }

        public Category Category { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public bool IsValid()
        {
            return UnitPrice >= 0 && UnitsInStock >= 0;
        }
    }
}