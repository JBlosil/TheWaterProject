using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheWaterProject.Models;

public partial class Product
{
    [Key]
    public int product_ID { get; set; }

    public string? Name { get; set; }

    public int? Year { get; set; }

    public int? Num_Parts { get; set; }

    public int? Price { get; set; }

    public string? img_link { get; set; }

    public string? Primary_Color { get; set; }

    public string? Secondary_Color { get; set; }

    public string? Description { get; set; }

    public string? Category { get; set; }
}
