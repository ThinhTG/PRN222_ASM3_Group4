namespace BusinessObject.Models
{
    public class Member
    {
        public int MemberId { get; set; } 
        public string Email { get; set; } 
        public string CompanyName { get; set; } 
        public string City { get; set; } 
        public string Country { get; set; } 
        public string Password { get; set; } 

        
        public List<Order> Orders { get; set; } = new List<Order>();

        
        public bool IsValidEmail()
        {
            return !string.IsNullOrEmpty(Email) && Email.Contains("@") && Email.EndsWith(".com");
        }

        
        public bool IsValidPassword()
        {
            return !string.IsNullOrEmpty(Password) && Password.Length >= 6 && Password.Length <= 30;
        }
    }
}