using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<SqlConnection>(provider => new SqlConnection(connectionString));
builder.Services.AddTransient<ISqlCommandFactory, SqlCommandFactory>();
builder.Services.AddSingleton<ITelefoneRepository, TelefoneRepository>();
builder.Services.AddSingleton<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddSingleton(typeof(IPessoaRepository<>), typeof(PessoaRepository<>));
builder.Services.AddSingleton(typeof(IPessoaFisicaRepository<>), typeof(PessoaFisicaRepository<>));
builder.Services.AddSingleton(typeof(IPessoaJuridicaRepository<>), typeof(PessoaJuridicaRepository<>));
builder.Services.AddSingleton<IClienteFisicoRepository, ClienteFisicoRepository>();
builder.Services.AddSingleton<IClienteJuridicoRepository, ClienteJuridicoRepository>();
builder.Services.AddSingleton<IClienteRepository, ClienteRepository>();
builder.Services.AddSingleton<IFornecedorRepository, FornecedorRepository>();
builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddSingleton<ICargoRepository, CargoRepository>();
builder.Services.AddSingleton<IFuncionarioRepository, FuncionarioRepository>();
builder.Services.AddSingleton<IUnidadeRepository, UnidadeRepository>();
builder.Services.AddSingleton<IProdutoRepository, ProdutoRepository>();


var app = builder.Build();

app.UseExceptionHandler("/Home/Error");

//app.UseAuthentication();
//app.UseAuthorization();

app.MapDefaultControllerRoute();
app.Run();
