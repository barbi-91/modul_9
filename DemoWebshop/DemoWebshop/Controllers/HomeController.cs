using DemoWebshop.Data;
using DemoWebshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DemoWebshop.Controllers
{
    //Atribut Authorize moze se primjeniti na klase ili ackije klase
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        //Depandacy uiinjection za objekt klase ApplicationDbContext
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index(string? searchQuery, int orderBy = 0)
        {
            //int pageNumber = 1;

            // 1. Ako je parametra "serachQuery" prazan ili ne postoji, prikazi sve proizvode
            List<Product> products = _dbContext.Products.ToList();

            // 2. Ako je parametra "serachQuery" postoji  i nije prazan, filtriraj proizvode(pretrazi kljucnu rijec u naslovu)
            //Mi ovdje mozemo odamhj pvucemo sve proizvide iz tablice - OVAJ PRISTUP NIJE NAJBOLJI U SVIM NACINIMA, AKO IMAMO MILUIN PROIZVODA SVIH MILIUN PROIZVODA IDE U MEMORIJU I TO PREOPTERETI CIJELI SUSTAV
            //OGROMNE ZAPISE ODMAH FILTRIRATI - DBCONTEXT 
            if (!String.IsNullOrWhiteSpace(searchQuery))
            {
                //vraca sve- nema pretrazenog
                products = products.Where(p => p.Title.ToLower().Contains(searchQuery.ToLower())).ToList();
            }

            //0 -zadani prikaz rezultata
            //1 -sortiranje po naslovu uzlazno
            //2 -sortiranje po naslovu silazno
            //2 -sortiranje po cijeni uzlazno
            //2 -sortiranje po cijeni silazno
            switch (orderBy)
            {
                case 1: products = products.OrderBy(p => p.Title).ToList(); break;
                case 2: products = products.OrderByDescending(p => p.Title).ToList(); break;
                case 3: products =  products.OrderBy(p => p.Price).ToList(); break;
                case 4: products = products.OrderByDescending(p => p.Price).ToList(); break;
            }


            //obavzeno u view moramo proslijedit kolekciju proizvoda
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}