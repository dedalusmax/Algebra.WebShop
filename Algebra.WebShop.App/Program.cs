using Algebra.WebShop.App.Data;
using Algebra.WebShop.App.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Algebra.WebShop.App;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.ConfigureServices(builder.Configuration, builder.Environment);

        var app = builder.Build();

        app.Configure();

        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        //context.Database.EnsureCreated();
        context.Database.Migrate();

        app.Run();
    }
}
