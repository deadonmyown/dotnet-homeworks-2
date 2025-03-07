using System.Diagnostics.CodeAnalysis;
using Hw8.Calculator;
using Hw8.Parser;

namespace Hw8;

[ExcludeFromCodeCoverage]
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<ICalculator, Calculator.Calculator>();
        builder.Services.AddScoped<IParser, Parser.Parser>();
        builder.Services.AddMiniProfiler();

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
        app.UseMiniProfiler();
        
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Calculator}/{action=Index}");
        app.MapControllerRoute(
            name: "calculate",
            pattern: "{controller=Calculator}/{action=Calculate}");

        app.Run();
    }
}