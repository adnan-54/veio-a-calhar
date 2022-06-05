using System.Data.SqlClient;
using VeioACalhar.Commands;
using VeioACalhar.Middlewares;
using VeioACalhar.Repositories;
using VeioACalhar.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient(provider => new SqlConnection(connectionString));
builder.Services.AddTransient<ISqlCommandFactory, SqlCommandFactory>();
builder.Services.AddSingleton(typeof(IPessoaRepository<>), typeof(PessoaRepository<>));
builder.Services.AddSingleton(typeof(IPessoaFisicaRepository<>), typeof(PessoaFisicaRepository<>));
builder.Services.AddSingleton(typeof(IPessoaJuridicaRepository<>), typeof(PessoaJuridicaRepository<>));
builder.Services.AddSingleton<IClienteRepository, ClienteRepository>();
builder.Services.AddSingleton<IClienteRepository, ClienteRepository>();
builder.Services.AddSingleton<IFornecedorRepository, FornecedorRepository>();
builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddSingleton<ICargoRepository, CargoRepository>();
builder.Services.AddSingleton<IFuncionarioRepository, FuncionarioRepository>();
builder.Services.AddSingleton<IUnidadeRepository, UnidadeRepository>();
builder.Services.AddSingleton<IProdutoRepository, ProdutoRepository>();
builder.Services.AddSingleton<IFormaPagamentoRepository, FormaPagamentoRepository>();
builder.Services.AddSingleton<IParcelaRepository, ParcelaRepository>();
builder.Services.AddSingleton<IPagamentoRepository, PagamentoRepository>();
builder.Services.AddSingleton<IStatusTransacaoRepository, StatusTransacaoRepository>();
builder.Services.AddSingleton<IProdutoTransacaoRepository, ProdutoTransacaoRepository>();
builder.Services.AddSingleton(typeof(ITransacaoRepository<>), typeof(TransacaoRepository<>));
builder.Services.AddSingleton<IVendaRepository, VendaRepository>();
builder.Services.AddSingleton<IVendaClienteRepository, VendaClienteRepository>();
builder.Services.AddSingleton<IVendaFuncionarioRepository, VendaFuncionarioRepository>();
builder.Services.AddSingleton<ICompraRepository, CompraRepository>();
builder.Services.AddSingleton<IUserService, UserSerivce>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseMiddleware<LoginRequiredMiddleware>();
app.UseMiddleware<NotFoundMiddleware>();

app.Run();
