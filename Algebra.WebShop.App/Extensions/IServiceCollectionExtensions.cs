using Algebra.WebShop.App.Data;
using Algebra.WebShop.App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Algebra.WebShop.App.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.

        services.AddHttpContextAccessor();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(1);
            options.Cookie.Name = ".Algebra.WebShop.Session";
            options.Cookie.IsEssential = true;
        });

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddDefaultIdentity<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdminRole", policy => policy
                .RequireAuthenticatedUser()
                .RequireRole("Admin"));
        });

        services.AddControllersWithViews();

        return services;
    }
}
