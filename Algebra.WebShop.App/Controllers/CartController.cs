using Algebra.WebShop.App.Data;
using Algebra.WebShop.App.Extensions;
using Algebra.WebShop.App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Algebra.WebShop.App.Controllers
{
    [Authorize]
    public class CartController(ApplicationDbContext context) : Controller
    {
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetCart();

            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var cart = HttpContext.Session.GetCart();

            if (cart.Items.Count == 0)
            {
                var item = new CartItem
                {
                    Product = context.Products.Find(productId)!,
                    Quantity = 1
                };

                cart.Items.Add(item);
            }
            else
            {
                if (cart.Items.Any(x => x.Product.Id == productId))
                {
                    var item = cart.Items.Single(x => x.Product.Id.Equals(productId));
                    item.Quantity++;
                } 
                else
                {
                    var item = new CartItem
                    {
                        Product = context.Products.Find(productId)!,
                        Quantity = 1
                    };

                    cart.Items.Add(item);
                }
            }

            HttpContext.Session.SetCart(cart);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetCart();

            if (cart.Items.Any(x => x.Product.Id == productId))
            {
                var item = cart.Items.Single(x => x.Product.Id.Equals(productId));
                cart.Items.Remove(item);
            }

            HttpContext.Session.SetCart(cart);

            return RedirectToAction(nameof(Index));
        }
    }
}
