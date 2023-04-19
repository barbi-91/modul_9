using DemoWebshop.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebshop.Controllers
{
    public class OrderController : Controller
    {

        //Ključ nase sesije za kosaricu -const samo jednom podesi i ona je samo read only
        public const string sessionCartKey = "_cart"; 

        //GET: Order/Checkout
        public IActionResult Checkout()
        {

            //Korak 1: pronadi sesiju i provjeri ako postoj barem jedan proizvod u kosarici
            List<CartItem> cart = HttpContext.Session.GetCartObjectFromJson(sessionCartKey);

            if (cart.Count <= 0)
            {
                return RedirectToAction("Index", "Home");
            }


            //Korak2: definiraj ViewBag za ispis poruka
            ViewBag.CheckoutMessages = TempData["CheckoutMessages"] as string ?? "";

            return View(cart);
        }

        //TODO: CreateOrder(Order newOrder)
    }
}
