public class CheckoutViewModel
{
    // Form fields
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string ShippingAddress { get; set; }
    public string CountryOfTransaction { get; set; }
    public string CardType { get; set; }
    public string Bank { get; set; }
    public int Age { get; set;  }
    public string Gender { get; set; }
    public string Email { get; set; }

    // Dropdown data or other additional data
    public Dictionary<string, string> Countries { get; set; }
    public Dictionary<string, string> Banks { get; set; }
    public Dictionary<string, string> CardTypes { get; set; }
}


