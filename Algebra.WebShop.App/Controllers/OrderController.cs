using Algebra.WebShop.App.Data;
using Algebra.WebShop.App.Extensions;
using Algebra.WebShop.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Algebra.WebShop.App.Controllers
{
    public class OrderController(ApplicationDbContext context) : Controller
    {
        public IActionResult Index(bool? success)
        {
            ViewData["Success"] = success;

            var cart = HttpContext.Session.GetCart();

            ViewData["Cart"] = cart;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CustomerFirstName,CustomerLastName,CustomerEmailAddress,CustomerPhoneNumber,CustomerAddress")] Order order)
        {
            ModelState.Remove("Items");

            if (ModelState.IsValid)
            {
                // TODO: implementirati transakciju

                var cart = HttpContext.Session.GetCart();

                order.Total = cart.GrandTotal;

                context.Orders.Add(order);
                context.SaveChanges();

                if (cart.Items.Count > 0)
                {
                    foreach (var cartItem in cart.Items)
                    {
                        var item = new OrderItem()
                        {
                            OrderId = order.Id,
                            Price = cartItem.Product.Price,
                            Quantity = cartItem.Quantity,
                            Total = cartItem.Total,
                            ProductId = cartItem.Product.Id
                        };

                        context.OrderItems.Add(item);
                    }

                    context.SaveChanges();
                }

                HttpContext.Session.ClearCart();

                return RedirectToAction(nameof(Index), new { success = true });
            }
            else
            {
                foreach (var item in ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        Console.WriteLine(error);
                    }
                }

                // ViewData["Errors"] = error;
            }

            return View();
        }
    }
}
