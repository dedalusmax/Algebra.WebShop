using Algebra.WebShop.App.Extensions;
using Algebra.WebShop.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Algebra.WebShop.App.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetCart();

            ViewData["Cart"] = cart;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CustomerFirstName,CustomerLastName,CustomerEmailAddress,CustomerPhoneNumber,CustomerAddress")] Order order)
        {
            return View(order);
        }
    }
}
