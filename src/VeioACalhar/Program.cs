using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<SqlConnection>(provider => new SqlConnection(connectionString));
builder.Services.AddSingleton<ISqlCommandFactory, SqlCommandFactory>();
builder.Services.AddSingleton<ITelefoneRepository, TelefoneRepository>();
builder.Services.AddSingleton<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddSingleton(typeof(IPessoaRepository<>), typeof(PessoaRepository<>));
builder.Services.AddSingleton(typeof(IPessoaFisicaRepository<>), typeof(PessoaFisicaRepository<>));
builder.Services.AddSingleton(typeof(IPessoaJuridicaRepository<>), typeof(PessoaJuridicaRepository<>));
builder.Services.AddSingleton<IClienteFisicoRepository, ClienteFisicoRepository>();
builder.Services.AddSingleton<IClienteJuridicoRepository, ClienteJuridicoRepository>();
builder.Services.AddSingleton<IFornecedorRepository, FornecedorRepository>();


var app = builder.Build();

app.UseExceptionHandler("/Home/Error");

//app.UseAuthentication();
//app.UseAuthorization();

app.MapDefaultControllerRoute();
app.Run();
