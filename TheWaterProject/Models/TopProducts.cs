using System.ComponentModel.DataAnnotations;

namespace TheWaterProject.Models;

public class TopProducts
{
    [Key]
    public int product_ID { get; set; }
    
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Price { get; set; }

    public string? img_link { get; set; }
    
}