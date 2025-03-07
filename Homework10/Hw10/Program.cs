// dotcover disable
using System.Diagnostics.CodeAnalysis;
using Hw10.Configuration;
using Hw10.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Hw10
{
    [ExcludeFromCodeCoverage]
    public partial class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMemoryCache().AddControllersWithViews();
            builder.Services
                .AddMathCalculator()
                .AddCachedMathCalculator();
            
            /*builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));*/

            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Calculator}/{action=Index}/{id?}");
            app.Run();
        }
    }
}