using System.Data.SqlClient;
using VeioACalhar.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IDbConnection, SqlDbConnection>(serviceProvider =>
{
    var logger = serviceProvider.GetRequiredService<ILogger<SqlDbConnection>>();
    var connection = new SqlConnection(connectionString);
    return new SqlDbConnection(connection, logger);
});

var app = builder.Build();

app.UseExceptionHandler("/Home/Error");

//app.UseAuthentication();
//app.UseAuthorization();

app.MapDefaultControllerRoute();
app.Run();
