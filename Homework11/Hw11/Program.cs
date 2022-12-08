using System.Diagnostics.CodeAnalysis;
using Hw11.Configuration;
using Hw11.Exceptions;

namespace Hw11
{
    [ExcludeFromCodeCoverage]
    public partial class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddMathCalculator();
            builder.Services.AddTransient<IExceptionHandler, ExceptionHandler>();
            builder.Services.AddTransient<ILogger<ExceptionHandler>, Logger<ExceptionHandler>>();

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
                pattern: "{controller=Calculator}/{action=Calculator}/{id?}");

            app.Run();
        }
    }
}