using Algebra.WebShop.App.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Algebra.WebShop.App.Views.Shared;

//[ViewComponent(Name = "ShoppingCart")]
public class ShoppingCart : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var cart = HttpContext.Session.GetCart();

        return View("Default", cart);
    }
}
