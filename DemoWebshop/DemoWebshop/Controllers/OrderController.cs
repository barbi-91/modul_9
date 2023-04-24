using DemoWebshop.Areas.Identity.Data;
using DemoWebshop.Data;
using DemoWebshop.Models;
using DemoWebshop.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace DemoWebshop.Controllers
{
    public class OrderController : Controller
    {

        //Ključ nase sesije za kosaricu -const samo jednom podesi i ona je samo read only
        public const string sessionCartKey = "_cart";

        //Objekt za pristu bazi podataka
        private readonly ApplicationDbContext _dbContext;

        //Objekt za pristup prijavljenom korisniku
        private readonly UserManager<ApplicationUser> _userNManager;


        public OrderController(ApplicationDbContext dbContext, UserManager<ApplicationUser> user)
        {
            _dbContext = dbContext;
            _userNManager= user;
        }


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

        //CreateOrder(Order newOrder)

        [HttpPost]
        public IActionResult CreateOrder(Order newOrder)
        {
            //1. Korak -Provjera ako je kosarica prazna
            //2. Korak -Provjeri ako je model klase validan (requerd i ostala polja)
            //3. Korak -Pohrana u bazu, cišcenje kosarice, preusmjeravanje, itd...

            List<CartItem> cart = HttpContext.Session.GetCartObjectFromJson(sessionCartKey);
            if (cart.Count <= 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var modelErrors = new List<string>();
            if (ModelState.IsValid)
            {
                //true - sva svojstva su validna
                //Pretvaramo se da svi nasi proizvodi imaju Pdv uracunat u cijenu!
                newOrder.Subtotal = 0;
                newOrder.Tax = 0;
                newOrder.Total = cart.Sum(item => item.GetTotal());

                //Uz pomoc uUser svojstva je moguce dobiti Id korisnika (ako je prijavljen)
                newOrder.UserId = _userNManager.GetUserId(User);
                //samo za one kojima model kalse nije dobar
                newOrder.User = null; 

                _dbContext.Orders.Add(newOrder);
                _dbContext.SaveChanges();
                foreach (var cartItem in cart)
                {
                    OrderItem newOrderItem = new OrderItem()
                    {
                        OrderId = newOrder.Id,
                        ProductId = cartItem.Product.Id,
                        Price = cartItem.Product.Price,
                        Quantity = cartItem.Quantity,
                        Total = cartItem.GetTotal()
                    };
                    _dbContext.OrderItems.Add(newOrderItem);
                }
                _dbContext.SaveChanges();

                HttpContext.Session.SetCartObjectAsJson(sessionCartKey, "");

                TempData["ThankYouMessage"] = "Thank you for ordering";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //false -neki podatak nije validan
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        modelErrors.Add(error.ErrorMessage);
                    }
                }
            }

            /*
             * primjer
             * Erro email.
             * Error first name,
             * Error last name...
             * 
             * Resultat: Error email <br> Error frist name <br> Error last name <br>
             */


            TempData["CheckoutMessages"] = String.Join("<br/>", modelErrors);
            return RedirectToAction("Checkout");
        }
    }
}
