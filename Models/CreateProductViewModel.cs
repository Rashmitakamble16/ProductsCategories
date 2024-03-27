namespace ASP.NET_MVC_Project.Models
{
    public class CreateProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; } // Foreign key
    }
}
