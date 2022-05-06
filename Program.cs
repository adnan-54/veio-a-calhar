namespace VeioACalhar;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();

        WebApplication? app = builder.Build();
        app.MapDefaultControllerRoute();
        // app.UseAuthentication();

        app.Run();
    }
}