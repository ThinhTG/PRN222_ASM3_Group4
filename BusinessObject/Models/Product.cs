
namespace BusinessObject.Models
{
    public class Product
    {
        public int ProductId { get; set; } 
        public int CategoryId { get; set; } 
        public string ProductName { get; set; } 
        public string Weight { get; set; } 
        public decimal UnitPrice { get; set; } 
        public int UnitsInStock { get; set; } 
        
        public Category Category { get; set; }

       
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        
        public bool IsValid()
        {
            return UnitPrice >= 0 && UnitsInStock >= 0;
        }
    }
}