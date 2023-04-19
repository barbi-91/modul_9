using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DemoWebshop.Data;
using DemoWebshop.Areas.Identity.Data;
using System.Globalization;

namespace DemoWebshop;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Dohvat connection stringa
        var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

        //Servis za kreiranje resursa oobjekta klase konteksta      ---db contex radi na bilo kojoj bazi ovdje korisitmo za sql server        
        builder.Services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(connectionString));

        //Servis koji kaze kako je klasa ApplicationUser glavna za identifikaciju korisnika
        builder.Services.AddDefaultIdentity<ApplicationUser>(
            options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>() // postavka za dodavanje uloge(role) uz pomoc kalse identity
            .AddEntityFrameworkStores<ApplicationDbContext>();


        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // 1 .Kreiranje servisa za korsintenje RazorPageopcija
        builder.Services.AddRazorPages(); //bez toga ne funkcionira @page u login.cshtml.cs

        //nakon razorPages--- ako zelimo narediti da se lozinka mora upisati na odredeni nacin
        builder.Services.Configure<IdentityOptions>(
            options =>
            {
                //Osnovne postavke za lozinku (samo za vjezbu)
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 7; // ne moze biti manje od 6
            }
        );

        //Kreiraj servis za sesiju
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        //postavke aplikacje za rukovanje deimalnim vrijdnostima-primjena globalno
        var ci = new CultureInfo("de-De");
        ci.NumberFormat.NumberDecimalSeparator = ".";
        ci.NumberFormat.CurrencyDecimalSeparator = ".";

        app.UseRequestLocalization(
                new RequestLocalizationOptions
                {
                    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(ci),
                    SupportedCultures = new List<CultureInfo> { ci },
                    SupportedUICultures = new List<CultureInfo> { ci },
                }
            );


        app.UseRouting();
        app.UseAuthentication();

        app.UseAuthorization();

        //dodavanje sesije
        app.UseSession();

        // podesavanje uloge admina

        app.MapAreaControllerRoute(
            name: "Admin",
            areaName: "Admin",
            pattern : "admin/{Controller}/{action}/{id?}"
            ) ;

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        // 2. Korak je mapiranje razor pagesa
        app.MapRazorPages();

        app.Run();
    }
}