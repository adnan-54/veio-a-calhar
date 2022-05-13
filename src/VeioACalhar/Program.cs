using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<SqlConnection>(provider => new SqlConnection(connectionString));
builder.Services.AddSingleton<ISqlCommandFactory, SqlCommandFactory>();
builder.Services.AddSingleton<ICargoRepository, CargoRepository>();
builder.Services.AddSingleton<ITelefoneRepository, TelefoneRepository>();
builder.Services.AddSingleton<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddSingleton<IPessoaRepository, PessoaRepository>();
builder.Services.AddSingleton<IPessoaFisicaRepository, PessoaFisicaRepository>();
builder.Services.AddSingleton<IPessoaJuridicaRepository, PessoaJuridicaRepository>();
builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddSingleton<IFuncionarioRepository, FuncionarioRepository>();


var app = builder.Build();

app.UseExceptionHandler("/Home/Error");

//app.UseAuthentication();
//app.UseAuthorization();

app.MapDefaultControllerRoute();
app.Run();
