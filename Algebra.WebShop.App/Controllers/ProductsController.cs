using Microsoft.AspNetCore.Mvc;

namespace Algebra.WebShop.App.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
