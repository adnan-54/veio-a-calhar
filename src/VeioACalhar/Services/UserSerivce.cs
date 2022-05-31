using VeioACalhar.Models;
using VeioACalhar.Repositories;

namespace VeioACalhar.Services;

internal class UserSerivce : IUserService
{
    private readonly IUsuarioRepository usuarioRepository;
    private readonly IHttpContextAccessor httpContext;

    public UserSerivce(IUsuarioRepository usuarioRepository, IHttpContextAccessor httpContext)
    {
        this.usuarioRepository = usuarioRepository;
        this.httpContext = httpContext;
        CurrentUser = new();
    }

    public Usuario CurrentUser { get; private set; }

    protected ISession? Session => httpContext.HttpContext?.Session;

    public Usuario SignUp(string login, string password)
    {
        var usuario = usuarioRepository.Get(login);
        if (usuario.Id != 0)
            throw new Exception("Usuário já existe");

        return usuarioRepository.Create(new Usuario { Login = login }, HashPassword(password));
    }

    public void SignIn(string login, string password)
    {
        if (Session is null)
            throw new Exception("Não há sessão ativa");

        if (Session.GetInt32("Usuario").HasValue)
            throw new Exception("Usuário já está logado");

        CurrentUser = GetUser(login, password);

        Session.SetInt32("Usuario", CurrentUser.Id);
    }

    public void SignOut()
    {
        if (Session is null)
            throw new Exception("Não há sessão ativa");

        Session.Clear();
        CurrentUser = new();
    }

    private Usuario GetUser(string login, string password)
    {
        var user = usuarioRepository.Get(login);

        if (user.Id == 0)
            throw new Exception("Usuário ou senha invalidos");

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            throw new Exception("Usuário ou senha invalidos");

        return user;
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}
