namespace Algebra.WebShop.App.Models;

public record CartItem
{
    public Product Product { get; set; }

    public decimal Quantity { get; set; }

    public decimal Total => Product.Price * Quantity;
}

public record Cart
{
    public List<CartItem> Items { get; set; } = [];

    public decimal GrandTotal => Items.Count == 0 ? 0 : Items.Sum(x => x.Total);
}