using System.ComponentModel.DataAnnotations;

namespace ASP.NET_MVC_Project.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        // Navigation property
        //public virtual ICollection<Product> Products { get; set; }
    }
}
