using Algebra.WebShop.App.Data;
using Algebra.WebShop.App.Extensions;
using Algebra.WebShop.App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Claims;

namespace Algebra.WebShop.App.Controllers
{
    [Authorize]
    public class OrderController(ApplicationDbContext context) : Controller
    {
        public IActionResult Index(bool? success)
        {
            ViewData["Success"] = success;

            var cart = HttpContext.Session.GetCart();

            ViewData["Cart"] = cart;

            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = context.Users.Find(userId);

            var order = new Order();

            if (user != null)
            {
                order.CustomerFirstName = string.IsNullOrEmpty(user.FirstName) ? string.Empty : user.FirstName;
                order.CustomerLastName = string.IsNullOrEmpty(user.LastName) ? string.Empty : user.LastName;
                order.CustomerPhoneNumber = string.IsNullOrEmpty(user.PhoneNumber) ? string.Empty : user.PhoneNumber;
                order.CustomerEmailAddress = string.IsNullOrEmpty(user.Email) ? string.Empty : user.Email;
                order.CustomerAddress = string.IsNullOrEmpty(user.Address) ? string.Empty : user.Address;
            }

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CustomerFirstName,CustomerLastName,CustomerEmailAddress,CustomerPhoneNumber,CustomerAddress")] Order order)
        {
            ModelState.Remove("Items");
            ModelState.Remove("User");
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                // TODO: implementirati transakciju

                var cart = HttpContext.Session.GetCart();
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                order.Total = cart.GrandTotal;
                order.DateTimeCreated = DateTime.UtcNow;
                order.UserId = userId!;

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
                var errors = new List<string>();
                foreach (var item in ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                return RedirectToAction(nameof(Index), new { success = false, errors });
            }
        }

        [HttpPost]
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
