using System.ComponentModel.DataAnnotations;

namespace TheWaterProject.Models.ViewModels;

public class ProductsListViewModel
{
    public IQueryable<Product> Products { get; set; }
    public IQueryable<TopProducts> TopProducts { get; set; }
    public IQueryable<Customer> Customers { get; set; }
    public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    public IQueryable<Order> Orders { get; set; }
    public IQueryable<LineItem> LineItems { get; set; }
    public string? ProductCategory { get; set;}
    public string? ProductColor { get; set; }
    public string? UserEmail { get; set; }
    public int? CustomerId { get; set; }
    public List<Product> RecommendedProducts { get; set; }
    
    public IEnumerable<ProductDetailsViewModel> ProductsList { get; set; }
    
    public class ProductDetailsViewModel
    {
        [Key]
        public int product_ID { get; set; }

        public string? Name { get; set; }

        public int? Year { get; set; }

        public int? Num_Parts { get; set; }

        public int Price { get; set; }

        public string? img_link { get; set; }

        public string? Primary_Color { get; set; }

        public string? Secondary_Color { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }
    }
}