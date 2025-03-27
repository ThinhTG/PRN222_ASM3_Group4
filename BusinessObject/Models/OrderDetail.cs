
namespace BusinessObject.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; } 
        public int ProductId { get; set; } 
        public decimal UnitPrice { get; set; } 
        public int Quantity { get; set; } 
        public float Discount { get; set; }

       
        public Order Order { get; set; }
        
        public Product Product { get; set; }
        
        public bool IsValid()
        {
            return Quantity > 0 && UnitPrice >= 0 && Discount >= 0 && Discount <= 1;
        }
    }
}