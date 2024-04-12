using System.ComponentModel.DataAnnotations;

namespace TheWaterProject.Models;

public class UserTopProducts
{
    [Key]
    public int product_ID { get; set; }
    public int Rec1 { get; set; }
    public int Rec2 { get; set; }
    public int Rec3 { get; set; }
    public int Rec4 { get; set; }
    public int Rec5 { get; set; }
}