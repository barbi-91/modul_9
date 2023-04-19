using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoWebshop.Data;
using DemoWebshop.Models;
using Microsoft.AspNetCore.Authorization;

namespace DemoWebshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    //dodati authorize da zastitimo klasu
    [Authorize(Roles = "Admin")] // ovo se moze prosiriti i na ogranicenja sa rolama! 
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
              return _context.Products != null ? 
                          View(await _context.Products.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Products'  is null.");
        }




        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }




        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string ?? "";
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Title,Description,Sku,InStock,Price,Image")] Product product,
            int[] categoryIds,
            IFormFile Image
            )
        {

            // 1. Korak: provjeri ako je parametar categoryIds prazan ili null
            if(categoryIds.Length == 0 || categoryIds == null)
            {
                // izbaci poruku kako kategorije se moraju odabrati -da odabere barem jednu kategoriju
                //TempData je kolekcija koja kreira kratkorocne poruke u sesiji izmedu dvije akcicije
                TempData["ErrorMessage"] = "Molimo odaberite minimalno jednu kategoriju!";
                return RedirectToAction(nameof(Create));
            }

            // 2. Korak - pohrani proizvod u tablicu i nakon toga povezi proizvod s odabranim kategorijama

            if (ModelState.IsValid)
            {

                //2.1.1 korak -pokusaj pohraniti sliku na disk! i spremi naziv slike u svojstvo product.image
                try
                {
                    //Primjer 1
                    var imageName = Image.FileName.ToLower();

                    //Primjer 2 
                    //var imageName = Image.FileName.ToLower().Replace(" ", "~").Replace("_", "-");

                    //Primjer 3 - 2023-04-06-18-04-04.jpg
                    //var getFimeExtension = Path.GetExtension(Image.FileName);
                    //var imageName = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + getFimeExtension;

                    //Odabir putanje gdje zelimo spremiti sliku
                    // REzultat: /wwwroot/images/products/100/naziv-slike.jpg
                    var saveImagePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot/images/products",
                        imageName
                        );

                    //kreiraj direktorije i poddirektorije unutar zadane putanje (wwwroot/images/products)
                    Directory.CreateDirectory(Path.GetDirectoryName(saveImagePath));

                    //ovdje se datoteka kopria fizicki unuatr zadane putanje (wwwroot/images/products) direktorija projekta
                    using (var stream = new FileStream(saveImagePath, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }
                    //U stupac tablice pohranjujemo samo naziv datoteke
                    product.Image = imageName;
                }
                catch (Exception ex)
                {

                    TempData["ErrorMessage"] = ex.Message;
                    return RedirectToAction(nameof(Create));
                }

                _context.Add(product);
                //_context.Products.Add(product)
                await _context.SaveChangesAsync(); //novi tred koji ce to pohraniti

                //nakon pohrane zapisa u tablicu EF Core ce u objektu popuniti pvrijdnost za svojstvo product.id
                // 2.1. korak .- povezi product .id sa stavkama niza categoryIds i pohrani sve u tablicu ProductCategories
                foreach (var categoryId in categoryIds)
                {
                    ProductCategory productCategory = new ProductCategory();
                    productCategory.CategoryId = categoryId;
                    productCategory.ProductId = product.Id;

                    _context.ProductCategories.Add(productCategory);
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index)); //koji se onda vrca u index kako bi prikazao taj zapis u tablici  
            }
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            //Dohvati Id-eve kategorija s kojima je proiivod povezan u tablici ProdcutCategories
            ViewBag.ProductCategories = _context.ProductCategories.Where(
                p => p.ProductId == product.Id
                ).Select(c => c.CategoryId).ToList();

            //Ako postoji error poruka, spremi u ViewBag svojstvo
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";  
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // 1 Postavljanje parametra u akaciju
        public async Task<IActionResult> Edit(
            int id, 
            [Bind("Id,Title,Description,Sku,InStock,Price,Image")] Product product,
            IFormFile? newImage,
            int[] categoryIds
            )
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            // 2 Provjeri ako je odabrana barem jedna kategorija
            if (categoryIds.Length == 0)
            {
                TempData["ErrorMessage"] = "Molimo odaberite minimalno jednu kategoriju!";
                return RedirectToAction(nameof(Edit), new { id = id });
            }


            if (ModelState.IsValid)
            {
                try
                {
                    // 3 Provjeri postoji li vrijdnost parametra newImage (znaci netko je odao novu sliku)
                    if (newImage != null)
                    {
                        //moramo zadati unikatan naziv slike najbolje sa sekundama
                        //Primjer: 2023-04-13-19-16-22_moja_slika1.jpg
                        //Primjer2: NazivKategorija-NazivProizvoda_IDProizvoda(jpg, png....)
                        // Primjer3: SKU(jpg...)
                        var newImageName = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "_" +
                            newImage.FileName.ToLower().Replace(" ", "_");

                        // 4 prekopriamo sa create dijela 
                        //Odabir putanje gdje zelimo spremiti sliku
                        // REzultat: /wwwroot/images/products/100/naziv-slike.jpg
                        var saveImagePath = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot/images/products",
                            newImageName
                            );

                        //kreiraj direktorije i poddirektorije unutar zadane putanje (wwwroot/images/products)
                        Directory.CreateDirectory(Path.GetDirectoryName(saveImagePath));

                        //ovdje se datoteka kopria fizicki unuatr zadane putanje (wwwroot/images/products) direktorija projekta
                        using (var stream = new FileStream(saveImagePath, FileMode.Create))
                        {
                            newImage.CopyTo(stream);
                        }
                        //U stupac tablice pohranjujemo samo naziv datoteke
                        product.Image = newImageName;

                    }
                    
                    _context.Update(product); //update proizvoda
                    await _context.SaveChangesAsync(); // pohrana u bazi poidataka

                    // 5 Azuiramo kategorije proizvoda u tablici productCatehggories
                    // 5.1. Izbrisi sve psotojece konekcije izmedu jategorije i proizvoda( ako postoje)
                    _context.ProductCategories.RemoveRange(_context.ProductCategories.Where(p => p.ProductId == id));
                    _context.SaveChanges();
                    // 5.2 Azuriraj nove podatke s vezuom izmedu proizvida i kategorije u tablici ProductCategories
                    foreach (var category in categoryIds)
                    {
                        ProductCategory productCategory = new ProductCategory();
                        productCategory.ProductId = product.Id;
                        productCategory.CategoryId = category;

                        _context.Add(productCategory);
                    }
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

           
            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            //EF ->automatski podesava vanjski kljuc na OnDelete: Cascade(postoji i onDelete: Restrict)
            //EF briše sve zapise gdje je Id proizvida vanjski kljuc i brise sam zapis proizvoda
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                if (!String.IsNullOrWhiteSpace(product.Image))
                {
                    //putanja na disku servera gdje se slika treba nalaziti
                    // C:/moj-folder/drugi-folder/projekt/wwwroot/images/products/slika.jpg
                    var deleteImageFromPath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwrooot/images/products",
                        product.Image
                    );
                    //Ako postoji datoteka, izbrisi je
                    if (System.IO.File.Exists(deleteImageFromPath))
                    {
                        System.IO.File.Delete(deleteImageFromPath);
                    }
                }
                _context.Products.Remove(product);

            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
