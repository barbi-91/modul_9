using DemoWebshop.Data;
using DemoWebshop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;

namespace DemoWebshop.Controllers
{
    public class CartController : Controller
    {
        //Prvo kreirati objekt klase za prstup baz i podataka
        public readonly ApplicationDbContext _dbContext;


        //Ključ nase sesije za kosaricu -const samo jednom podesi i ona je samo read only
        public const string sessionCartKey = "_cart";

        public CartController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        //GET Cart/Index
        public IActionResult Index()
        {

            //Korak 1: Provjeri kosaricu iz sesije
            List<CartItem> cart = HttpContext.Session.GetCartObjectFromJson(sessionCartKey);

            // KOrak 2: Provjeri errror poruku
            ViewBag.CartErrorMessage = TempData["CartErrorrMessage"] as string ?? "" ;


            return View(cart);
        }

        //TODO: AddToCart(int productId, decimal qunantity)
        //GET: Cart/AssToCart(int productId, decimal quantitiy)
        public IActionResult AddToCart(int productId, decimal quantity)
        {
            if (quantity <= 0)
            {
                return RedirectToAction(nameof(Index), "Home");
            }

            /*
             * 2 Moguca scenarija: 
             * - 1kosarica je prazna: 
             *          -a)kreiraj objekt klase CartItem i popuni ga sa podacima,dodaj u kolekciju, pa spremi u sesiju
             * - 2košarica nije prazna: 
             *          -a)proizvod postoji vec i azururaj kolicniu i pohrani oper sve u sesiju
             *          -b)proizvod ne postoji u kosarici , dodajga, azuriraj sve i dodaj u sesiju
             */

            // Korak 1: provjeri ako postoji proizvod
            var findProduct = _dbContext.Products.Find(productId);
            if (findProduct == null)
            {
                return RedirectToAction(nameof(Index), "Home");
            }


            // Korak 2: Provjeri sesiju
            // ova metoda vrca ili praznu ili punu kolekciju
            List<CartItem> cart = HttpContext.Session.GetCartObjectFromJson(sessionCartKey);




            // Korak 3: uvjeti za krositinje kosarice
            if (cart.Count == 0)
            {
                // Sto ako netko zeli vise proizvoda nego sto ih imamo dosutpno?
                if (quantity >findProduct.InStock)
                {
                    TempData["CartErrorMessage"] = $"Nije moguce dodati proizvdo u kosaricu! NA ZALIHI je dostupno {findProduct.InStock}";
                    return RedirectToAction(nameof(Index));
                }

                // Kreiraj novi objekt klase CartItem i popuni ga s podacima o proioizviodou i kolicini
                CartItem newItem = new CartItem()
                {
                    Product = findProduct,
                    Quantity = quantity
                };
                // Dodaj stavku u kolekciju kosarice
                cart.Add(newItem);
                //Azuriraj sesiju za kosaricu
                HttpContext.Session.SetCartObjectAsJson(sessionCartKey, cart);
            }
            else
            {
                //Ako proizvod nije u kosarici, kreiraj novi objekt klase CartItem , ako je u kosarici onda smao zauriraj kolicinu tog proizvoda
                var updateOrCreateItem = cart.Find(p => p.Product.Id == productId) ?? new CartItem();

                //Provjera kolicine
                //Primjer 1: U kosarici imamo 2 soka od jabuke , a InStock = 5
                //Primjer 2: U kosarici nemamo soka od jabuke , a InStock = 3
                if (quantity + updateOrCreateItem.Quantity > findProduct.InStock)
                {
                    TempData["CartErrorMessage"] = $"Nije moguce dodati odabranu kolicinu proizvoda. Na Zalihi je dostupno: {findProduct.InStock} proizvoda {findProduct.Title}";
                    return RedirectToAction(nameof(Index));
                }
                //Uvjet za azuiranje podataka sesije
                if (updateOrCreateItem.Quantity == 0)
                {
                    updateOrCreateItem.Product = findProduct;
                    updateOrCreateItem.Quantity = quantity;
                    cart.Add(updateOrCreateItem);
                }
                else
                {
                    updateOrCreateItem.Quantity += quantity;
                }

                //Azuriraj sesiju
                HttpContext.Session.SetCartObjectAsJson(sessionCartKey, cart);
            }


            return RedirectToAction(nameof(Index));
        }

        //TODO: RemoveFromCart(int productId)

        //GET: TestSession()

        public IActionResult TestSession()
        {

            //Primjer 1

            //Jednostavan primjer dodvanja sesije po kljucu i vrijdnosti
            HttpContext.Session.SetString("mojString", "Ovo je moja vrijdnost za string!");

            ViewBag.ReadSessionString = HttpContext.Session.GetString("sessionString");

            // AKo ovdje dodamo novu sesiju ona se prebrise 
            //Primjer azuiranja vrijdnsoti postijeg kljuca sesije
            HttpContext.Session.SetString("mojString", "Ja sam neki drugi text!");

            return View();
        }
    }
}
