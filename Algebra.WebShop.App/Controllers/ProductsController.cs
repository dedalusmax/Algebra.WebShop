using Algebra.WebShop.App.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Algebra.WebShop.App.Controllers
{
    public class ProductsController(ApplicationDbContext context) : Controller
    {
        public IActionResult Index(int? categoryId)
        {
            ViewData["Categories"] = new SelectList(context.Categories, "Id", "Name");

            var products = context.Products.ToList();

            if (categoryId != null)
            {
                products = (
                    from product in context.Products
                    join productCategory in context.ProductCategories on product.Id equals productCategory.ProductId
                    where productCategory.CategoryId == categoryId
                    select product
                    ).ToList();
            }

            return View(products);
        }
    }
}
