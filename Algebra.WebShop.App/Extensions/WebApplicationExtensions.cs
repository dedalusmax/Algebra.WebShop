using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Algebra.WebShop.App.Extensions;

public static class WebApplicationExtensions
{
    public static void Configure(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        var culture = new CultureInfo("hr-HR");
        culture.NumberFormat.NumberDecimalSeparator = ".";
        culture.NumberFormat.CurrencyDecimalSeparator = ".";

        app.UseRequestLocalization(new RequestLocalizationOptions()
        {
            DefaultRequestCulture = new RequestCulture(culture),
            SupportedCultures = [culture],
            SupportedUICultures = [culture]
        });

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSession();

        app.MapControllerRoute(
            name: "Admin",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();
    }
}
