using DemoWebshop.Areas.Identity.Data;
using DemoWebshop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DemoWebshop.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{

    //Mapiraj C# klase modela s tablicam u bazi podatka
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }



    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    //dz
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Data seeding for table Category
        Category vino = new Category() { Id = 1, Description = "Rose Dea Pannonium dobiva se od sorte Pinot crni, nježne je ružičaste boje, raskošnih voćnih aroma, mekano i skladno. Vino koje će zadovoljiti ljubitelje slatkastih, sočnih i voćnih rose vina.", Title = "Rose Dea Pannonium", Image = "https://webshop.rotodinamic.hr/image/cache/cache/2001-3000/2434/main/c826-022155-0-2-800x1200.jpg" };
        Category pivo = new Category() { Id = 2, Description = "U svijetu se najviše konzumiraju tzv. lager piva ili “piva donjeg vrenja” koja se dobivaju vrenjem pivske sladovine pomoću različitih sojeva čiste kulture kvasca vrste Saccharomyces uvarum.", Title = "Lager pivo", Image = "https://www.beerstyle.rs/wp-content/uploads/2018/07/lager_beerstyle_07_1499-1024x767.jpg" };
        Category odjeca = new Category() { Id = 3, Description = "Odjeća za slobodno vrijeme je odličan izbor kada osim atraktivnom izgleda trebaš i udobnost.", Title = "Odjeca za slobodno vrijeme", Image = "https://cdn.nexgeontools.com/ml2PH3yIFr8frQ6wnRUB4_Pb0Qc=/fit-in/0x0/nexgeontools%2Fcreatives%2F2023%2F03%2Fodjeca-za-slobodno-vrijeme-vk38-hrny7v%2Fimages%2F1%2Fimg_64117bc8096e21.96924669.png" };
        Category obuca = new Category() { Id = 4, Description = "Požurite po svoju sportsku kombinaciju i budite u trendu po povoljnim cijenama.", Title = "Sportska obuca", Image = "https://www.restoranloncic.com.hr/upload/1-cdn_127077/Mu%C5%A1ka-obu%C4%87a-za-tenis-%D0%BF%D0%B0%D1%80%D1%83%D1%81%D0%B8%D0%BD%D0%BE%D0%B2%D0%B0%D1%8F-sportska.jpg" };
        Category kozmetika = new Category() { Id = 5, Description = "Istražite svijet kozmetičkih proizvoda i uživajte u ekskluzivnim ponudama.", Title = "Puder", Image = "https://krenizdravo.dnevnik.hr/wp-content/uploads/2017/09/mineralna-kozmetika.jpg?x11092" };

        builder.Entity<Category>().HasData(vino, pivo, odjeca, obuca, kozmetika);

        //Data seeding for table Product
        Product posip = new Product()
        {
            Id = 1,
            Image = "https://plavakamenica.hr/wp-content/uploads/2019/07/rosei-lista-ljeto-2019.jpg",
            Description = "Ovo vino sa Korčule, punog tijela, jakih voćnih okusa i čvrste strukture, napunjeno je u svega nekoliko stotina buteljki, što objašnjava cijenu višu od Miravalove.",
            InStock = 5,
            Price = 15.50M,
            Title= "Nerica Crni pošip 2018, Korčula, 90 /100 ",
            Sku= "f54ddeda72"
        };
        Product lepiDecki = new Product()
        {
            Id = 2,
            Image = "https://www.promili.hr/web/image/product.template/2469/image_1024/%5B10664%5D%20Lepi%20De%C4%8Dki%20Tria%20De%20Los%20Muertos?unique=93f7694",
            Description = "Craft lager po originalnoj recepturi od 3 vrste hmelja i 3 vrste ječma, izraženog mirisa i slatkastog okusa.",
            InStock = 20,
            Price = 2.15M,
            Title = "Lepi Dečki Tria De Los Muertos",
            Sku = "f54ddeda73"
        };
        Product majica = new Product()
        {
            Id = 3,
            Image = "https://cdn.aboutstatic.com/file/6c84093b0cfbdff00554177c964feb05?brightness=0.96&quality=75&trim=1&height=480&width=360",
            Description = "Iriedaily Majice kratkih rukava za slobodno vrijeme.",
            InStock = 3,
            Price = 39.90M,
            Title = "Majica 'Hopi'",
            Sku = "f54ddeda74"
        };
        Product patike = new Product()
        {
            Id = 4,
            Image = "https://img.eobuwie.cloud/eob_product_512w_512h(b/7/a/3/b7a3409187765ea0fdfdfa35e5533ead4fcb4b70_01_0000301529996_rz.jpg,jpg)/obuca-nike-air-max-270-gs-wd-do6490-001-black-black-particle-grey.jpg",
            Description = "Unutrašnjost : Tekstil\r\nGornjište : 27% Sintetika, 73% Tekstil\r\nDonjište : Guma",
            InStock = 2,
            Price = 136.57M,
            Title = "NIKE AIR MAX 270 (GS)",
            Sku = "f54ddeda75"
        };
        Product puderTekuci = new Product()
        {
            Id = 5,
            Image = "https://miss7.24sata.hr/media/img/4b/41/57f9552057adc4c3d7fe.jpeg",
            Description = "Ako imate mješovitu do masniju kožu, ovo je savršen puder za vas, a sebum vam neće probijati kroz njega. Prekrivna moć je srednja do jača, ali osjećaj na licu je potpuno prirodan. Kožu će vam lagano matirati, ali ako je nakon nanošenja fiksirate transparentnim puderom i onda poprskate mineralnom vodicom za lice, dobit ćete savršeno prirodan look. S obzirom na vrlo pristupačnu cijenu i odličnu kvalitetu, ne čudi da je toliko omiljen.",
            InStock = 10,
            Price = 20.30M,
            Title = "Catrice HD Liquid Coverage",
            Sku = "f54ddeda76"
        };
        builder.Entity<Product>().HasData(posip, lepiDecki, majica, patike, puderTekuci);

        base.OnModelCreating(builder);
    }
}
