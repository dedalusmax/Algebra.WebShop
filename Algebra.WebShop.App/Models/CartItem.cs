namespace Algebra.WebShop.App.Models;

public record CartItem
{
    public Product Product { get; set; }

    public decimal Quantity { get; set; }

    public decimal Total => Product.Price * Quantity;
}
