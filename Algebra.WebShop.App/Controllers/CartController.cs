﻿using Algebra.WebShop.App.Data;
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

            if (cart.Count == 0)
            {
                var item = new CartItem
                {
                    Product = context.Products.Find(productId)!,
                    Quantity = 1
                };

                cart.Add(item);
            }
            else
            {
                if (cart.Any(x => x.Product.Id == productId))
                {
                    var item = cart.Single(x => x.Product.Id.Equals(productId));
                    item.Quantity++;
                } 
                else
                {
                    var item = new CartItem
                    {
                        Product = context.Products.Find(productId)!,
                        Quantity = 1
                    };

                    cart.Add(item);
                }
            }

            HttpContext.Session.SetCart(cart);

            return RedirectToAction(nameof(Index));
        }
    }
}
