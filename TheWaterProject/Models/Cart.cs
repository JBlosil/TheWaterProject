namespace TheWaterProject.Models;

public class Cart
{
    public List<CartLine> Lines { get; set; } = new List<CartLine>();

    public virtual void AddItem(Product product, int quantity)
    {
        CartLine? line = Lines.Where(x => x.Product.product_ID == product.product_ID)
            .FirstOrDefault();
        
        if (line == null)
        {
            Lines.Add(new CartLine
            {
                Product = product,
                Quantity = quantity
            });
        }
        else
        {
            line.Quantity += quantity;
        }
    }

    public virtual void RemoveLine(Product product) => Lines.RemoveAll(x => x.Product.product_ID == product.product_ID);

    public virtual void Clear() => Lines.Clear();

    public decimal CalculateTotal() => Lines.Sum(x => x.Product.Price * x.Quantity);

    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}