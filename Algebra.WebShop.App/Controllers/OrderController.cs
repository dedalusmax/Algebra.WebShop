using Algebra.WebShop.App.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Algebra.WebShop.App.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetCart();

            return View(cart);
        }
    }
}
