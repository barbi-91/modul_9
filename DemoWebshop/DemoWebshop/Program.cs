using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DemoWebshop.Data;
using DemoWebshop.Areas.Identity.Data;

namespace DemoWebshop;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Dohvat connection stringa
        var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");


        //Servis za kreiranje resursa oobjekta klase konteksta              
        builder.Services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(connectionString));
        //Servis koji kaze kako je klasa ApplicationUser glavna za identifikaciju korisnika

        builder.Services.AddDefaultIdentity<ApplicationUser>(
            options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>();

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // 1 .Kreiranje servisa za korsintenje RazorPageopcija
        builder.Services.AddRazorPages();

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

        app.UseRouting();
        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        // 2. Korak je mapiranje razor pagesa
        app.MapRazorPages();

        app.Run();
    }
}