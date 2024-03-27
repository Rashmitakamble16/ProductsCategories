namespace ASP.NET_MVC_Project.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; } // Foreign key

        // Navigation property
        public virtual Category Category { get; set; }
    }
}
