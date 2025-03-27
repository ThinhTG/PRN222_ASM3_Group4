
namespace BusinessObject.Models
{
    public class Order
    {
        public int OrderId { get; set; } 
        public int MemberId { get; set; } 
        public DateTime OrderDate { get; set; } 
        public DateTime? RequiredDate { get; set; } 
        public DateTime? ShippedDate { get; set; } 
        public decimal? Freight { get; set; } 

       
        public Member Member { get; set; }

        
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        // Business rule: Validate dates
        public bool AreDatesValid()
        {
            if (ShippedDate.HasValue && ShippedDate < OrderDate)
                return false;
            if (RequiredDate.HasValue && RequiredDate < OrderDate)
                return false;
            return true;
        }
    }
}